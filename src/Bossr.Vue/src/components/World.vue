<template>
  <div class="raids">
    <div class="columns is-multiline">
      <div class="column is-one-third" v-for="state in states">
        <div class="box">
          <span class="is-pulled-right"><b-icon icon="call_missed" />{{ state.missedRaids }}</span>
          <div v-for="spawn in state.raid.spawns" :key="spawn.id">
            <p><strong>{{ spawn.creature.name }}</strong></p>
            <p v-for="position in spawn.positions" :key="position.id">
              {{ position.name }}
              <b-icon icon="place" />
            </p>
            <hr>
          </div>
          <div>
            <p style="text-align: center; margin: 1.5rem"><b-icon icon="access_time" /> {{ getFormattedTime(state.expectedMin) }} - {{ getFormattedTime(state.expectedMax) }}</p>
            <progress class="progress" v-bind:value="getProgress(state.expectedMin, state.expectedMax)" max="100" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'

export default {
  data () {
    return {
      states: []
    }
  },
  created () {
    this.$http.get('http://localhost:5000/api/states/' + this.$route.params.id).then(response => {
      function compare (a, b) {
        if (a.raid.spawns[0].creature.name < b.raid.spawns[0].creature.name) {
          return -1
        }
        if (a.raid.spawns[0].creature.name > b.raid.spawns[0].creature.name) {
          return 1
        }
        return 0
      }
      this.states = response.body.sort(compare)
    })
  },
  methods: {
    getProgress (min, max) {
      let minUnix = moment(min).unix()
      let maxUnix = moment(max).unix()

      let now = moment().unix()

      let maxValue = maxUnix - minUnix
      let currentValue = now - minUnix

      return Math.round((currentValue / maxValue) * 100)
    },
    getFormattedTime (time) {
      return moment(time).fromNow()
    }
  }
}
</script>
