<template>
  <div>
    <section class="section">
      <div class="container">
        <a v-for="tag in tags" class="button" v-on:click="toggleActive(tag)" v-bind:class="{ 'is-success': tag.isActive }">{{ tag.name }}</a>
      </div>
    </section>
    <section class="section">
      <div class="container">
        <h2 class="title">Possible</h2>
        <h3 class="subtitle">Raids that can occur right now</h3>
        <div class="columns is-multiline">
          <possible-state-box 
            class="column is-one-third" 
            v-for="state in states"
            v-if="isSpawnable(state.expectedMin)" 
            :key="state.raid.id"
            :state="state">
          </possible-state-box>
        </div>
      </div>
    </section>
    <section class="section">
      <div class="container">
        <h2 class="title">Upcoming</h2>
        <h3 class="subtitle">Raids that soon can occur again</h3>
        <div class="columns is-multiline">
          <upcoming-state-box
            class="column is-one-third" 
            v-for="state in states"
            v-if="!isSpawnable(state.expectedMin)" 
            :key="state.raid.id"
            :state="state">
          </upcoming-state-box>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import moment from 'moment'
import PossibleStateBox from './PossibleStateBox'
import UpcomingStateBox from './UpcomingStateBox'

export default {
  components: {
    'possible-state-box': PossibleStateBox,
    'upcoming-state-box': UpcomingStateBox
  },
  data () {
    return {
      states: [],
      tags: []
    }
  },
  created () {
    this.$http.get(process.env.API_URL + 'api/states/' + this.$route.params.worldid).then(response => {
      this.states = response.body.sort(this.compareByCreatureName)
    })

    this.$http.get(process.env.API_URL + 'api/tags').then(response => {
      response.body.forEach(function (tag) {
        tag.isActive = false
      })
      this.tags = response.body
    })
  },
  methods: {
    compareByCreatureName (a, b) {
      if (a.raid.spawns[0].creature.name < b.raid.spawns[0].creature.name) {
        return -1
      }
      if (a.raid.spawns[0].creature.name > b.raid.spawns[0].creature.name) {
        return 1
      }
      return 0
    },
    isSpawnable (min) {
      return moment().diff(min) > 0
    },
    toggleActive (tag) {
      tag.isActive = !tag.isActive
      this.filterStates()
    },
    filterStates () {
      // Get state tags
      console.log(this.states)
      console.log(this.tags)
    }
  }
}
</script>
