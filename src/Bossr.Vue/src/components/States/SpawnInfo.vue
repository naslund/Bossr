<template>
  <div>
    <p>
      <strong>{{ spawn.creature.name }}</strong>
    </p>
    <position-info
      v-for="position in spawn.positions" 
      :position="position" 
      :key="position.id">
    </position-info>
    <hr>
  </div>
</template>

<script>
import PositionInfo from './PositionInfo'

export default {
  name: 'spawn-info',
  props: ['spawn'],
  components: {
    'position-info': PositionInfo
  }
}
</script>
