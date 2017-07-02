<template>
  <div>
    <section class="section">
      <div class="container">
        <h1 class="title">{{ world.name }}</h1>
        <input 
          class="input"
          type="text" 
          placeholder="Filter by creature or location"
          v-model="filter">
      </div>
    </section>
    <section class="section">
      <div class="container">
        <h2 class="title">Possible</h2>
        <h3 class="subtitle">Raids that can occur right now</h3>
        <div class="columns is-multiline">
          <possible-state-box 
            class="column is-one-third" 
            v-for="state in filteredStates"
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
            v-for="state in filteredStates"
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
      filteredStates: [],
      states: [],
      world: {},
      filter: ''
    }
  },
  created () {
    this.$http.get(process.env.API_URL + 'api/worlds/' + this.$route.params.worldid).then(response => {
      this.world = response.body
    })

    this.$http.get(process.env.API_URL + 'api/states/' + this.$route.params.worldid).then(response => {
      this.states = response.body.sort(this.compareByCreatureName)
      this.filterStatesByKeyword(this.filter)
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
    filterStatesByKeyword (keyword) {
      var normalizedKeyword = keyword.toLowerCase()
      var states = []

      this.states.forEach(function (state, index) {
        var isMatch = false
        state.raid.spawns.forEach(function (spawn) {
          if (spawn.creature.name.toLowerCase().indexOf(normalizedKeyword) > -1) {
            isMatch = true
          }
          spawn.positions.forEach(function (position) {
            if (position.name.toLowerCase().indexOf(normalizedKeyword) > -1) {
              isMatch = true
            }
          })
        })
        if (isMatch) {
          states.push(state)
        }
      })

      this.filteredStates = states
    }
  },
  watch: {
    'filter': function (keyword) {
      this.filterStatesByKeyword(keyword)
    }
  }
}
</script>
