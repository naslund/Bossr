<template>
  <p>
    <span>{{ position.name }}</span> <b-icon icon="place" />
  </p>
</template>

<script>
export default {
  name: 'position-info',
  props: ['position']
}
</script>
