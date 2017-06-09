<template>
  <div>
    <div class="box">
      <spawn 
        v-for="spawn in state.raid.spawns" 
        :spawn="spawn" 
        :key="spawn.id">
      </spawn>
      <div>
        <progress class="progress is-primary" max="100" v-bind:value="progressPercentage"></progress>
        <p class="has-text-centered">
          <b-icon icon="access_time" /> {{ timeRemaining }}
        </p>
        <missed-raids :missed-raids="state.missedRaids"></missed-raids>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'
import Spawn from './Spawn'
import MissedRaids from './MissedRaids'

export default {
  name: 'possible-state-box',
  components: {
    'spawn': Spawn,
    'missed-raids': MissedRaids
  },
  props: ['state'],
  computed: {
    timeRemaining () {
      return moment(this.state.expectedMax).fromNow(true) + ' remaining'
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
