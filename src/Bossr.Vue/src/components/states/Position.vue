<template>
  <p>
    <span>{{ position.name }}</span>
    <a v-on:click="mapModal(position.x, position.y, position.z)"><b-icon icon="place" /></a>
  </p>
</template>

<script>
import Map from '../Map'

export default {
  name: 'position',
  props: ['position'],
  methods: {
    mapModal (x, y, z) {
      this.$modal.open({
        component: Map,
        props: {
          x: x,
          y: y,
          z: z
        }
      })
    }
  }
}
</script>
