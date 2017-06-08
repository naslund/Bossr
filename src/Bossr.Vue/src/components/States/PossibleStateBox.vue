<template>
  <div>
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
        <progress class="progress is-primary" max="100" v-bind:value="progressPercentage"></progress>
        <p class="has-text-centered">
          <b-icon icon="access_time" /> {{ timeRemaining }}
        </p>
        <p class="has-text-centered">
          <b-icon icon="call_missed" /> {{ missedRaids }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'

export default {
  name: 'possible-state-box',
  props: ['state'],
  computed: {
    timeRemaining () {
      return moment(this.state.expectedMax).fromNow(true) + ' remaining'
    },
    missedRaids () {
      let message = this.state.missedRaids + ' missed raid'
      if (this.state.missedRaids !== 1) {
        message += 's'
      }

      return message
    },
    progressPercentage () {
      let minUnix = moment(this.state.expectedMin).unix()
      let maxUnix = moment(this.state.expectedMax).unix()

      let now = moment().unix()

      let maxValue = maxUnix - minUnix
      let currentValue = now - minUnix

      return Math.round((currentValue / maxValue) * 100)
    }
  }
}
</script>
