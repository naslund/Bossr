<template>
  <div>
    <iframe :src="mappedUrl"></iframe>
  </div>
</template>

<script>
export default {
  props: ['x', 'y', 'z'],
  computed: {
    mappedUrl () {
      return '/static/map/map.html#' + this.x + ',' + this.y + ',' + this.z + ':' + 1
    }
  }
}
</script>

<style>
iframe { 
  border: 0;
  width: 100%;
  min-height: 500px;
}
</style>
