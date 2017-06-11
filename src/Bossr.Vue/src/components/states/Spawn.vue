<template>
  <div>
    <p>
      <strong>{{ spawn.creature.name }}</strong>
    </p>
    <position
      v-for="position in spawn.positions" 
      :position="position" 
      :key="position.id">
    </position>
    <hr>
  </div>
</template>

<script>
import Position from './Position'

export default {
  name: 'spawn',
  props: ['spawn'],
  components: {
    'position': Position
  }
}
</script>
