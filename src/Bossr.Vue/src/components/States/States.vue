<template>
  <div>
    <section class="section">
      <div class="container">
        <h1 class="title">Possible</h1>
        <h2 class="subtitle">Raids that can occur right now</h2>
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
        <h1 class="title">Upcoming</h1>
        <h2 class="subtitle">Raids that soon can occur again</h2>
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
      states: []
    }
  },
  created () {
    this.$http.get(process.env.API_URL + 'api/states/' + this.$route.params.worldid).then(response => {
      console.log(response.body)
      this.states = response.body.sort(this.compareByCreatureName)
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
    }
  }
}
</script>
