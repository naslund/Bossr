<template>
  <div>
    <section class="hero is-success">
      <div class="hero-body">
        <div class="container">
          <h1 class="title">
            Possible
          </h1>
          <h2 class="subtitle">
            Raids that can occur right now
          </h2>
        </div>
      </div>
    </section>
    <section class="section">
      <div class="container">
        <div class="columns is-multiline">
          <div class="column is-one-third" v-for="state in states" v-if="isSpawnable(state.expectedMin)">
            <div class="box">
              <div v-for="spawn in state.raid.spawns" :key="spawn.id">
                <p><strong>{{ spawn.creature.name }}</strong></p>
                <p v-for="position in spawn.positions" :key="position.id">
                  {{ position.name }}
                  <b-icon icon="place" />
                </p>
                <hr>
              </div>
              <div>
                <progress class="progress is-primary" v-bind:value="getProgress(state.expectedMin, state.expectedMax)" max="100" />
                <p class="has-text-centered">
                  <b-icon icon="access_time" /> {{ getFormattedTime(state.expectedMax) }} remaining
                </p>
                <p class="has-text-centered">
                  <b-icon icon="call_missed" />{{ state.missedRaids }} missed raids
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section class="hero is-danger">
      <div class="hero-body">
        <div class="container">
          <h1 class="title">
            Upcoming
          </h1>
          <h2 class="subtitle">
            Raids that soon can occur again
          </h2>
        </div>
      </div>
    </section>
    <section class="section">
      <div class="container">
        <div class="columns is-multiline">
          <div class="column is-one-third" v-for="state in states" v-if="!isSpawnable(state.expectedMin)">
            <div class="box">
              <div v-for="spawn in state.raid.spawns" :key="spawn.id">
                <p><strong>{{ spawn.creature.name }}</strong></p>
                <p v-for="position in spawn.positions" :key="position.id">
                  {{ position.name }}
                  <b-icon icon="place" />
                </p>
                <hr>
              </div>
              <div>
                <p class="has-text-centered">
                  <b-icon icon="access_time" /> in {{ getFormattedTime(state.expectedMin) }}
                </p>
                <p class="has-text-centered">
                  <b-icon icon="call_missed" />{{ state.missedRaids }} missed raids
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
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
    isSpawnable (min) {
      return moment().diff(min) > 0
    },
    getProgress (min, max) {
      let minUnix = moment(min).unix()
      let maxUnix = moment(max).unix()

      let now = moment().unix()

      let maxValue = maxUnix - minUnix
      let currentValue = now - minUnix

      return Math.round((currentValue / maxValue) * 100)
    },
    getFormattedTime (time) {
      return moment(time).fromNow(true)
    }
  }
}
</script>
