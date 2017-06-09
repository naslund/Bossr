<template>
  <div>
    <div class="box">
      <spawn
        v-for="spawn in state.raid.spawns" 
        :spawn="spawn" 
        :key="spawn.id">
      </spawn>
      <div>
        <p class="has-text-centered">
          <b-icon icon="access_time" /> {{ timeUntilPossible }}
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
  name: 'upcoming-state-box',
  components: {
    'spawn': Spawn,
    'missed-raids': MissedRaids
  },
  props: ['state'],
  computed: {
    timeUntilPossible () {
      return moment(this.state.expectedMin).fromNow()
    }
  }
}
</script>
