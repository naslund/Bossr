<template>
  <div>
    <div class="box">
      <spawn-info 
        v-for="spawn in state.raid.spawns" 
        :spawn="spawn" 
        :key="spawn.id">
      </spawn-info>
      <div>
        <p class="has-text-centered">
          <b-icon icon="access_time" /> in {{ getFormattedTime(state.expectedMin) }}
        </p>
        <p class="has-text-centered">
          <b-icon icon="call_missed" /> {{ state.missedRaids }} missed raids
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'
import SpawnInfo from './SpawnInfo'

export default {
  name: 'upcoming-state-box',
  components: {
    'spawn-info': SpawnInfo
  },
  props: ['state'],
  methods: {
    getFormattedTime (time) {
      return moment(time).fromNow(true)
    }
  }
}
</script>
