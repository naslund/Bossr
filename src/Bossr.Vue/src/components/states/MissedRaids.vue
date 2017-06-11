<template>
  <p class="has-text-centered">
    <b-icon icon="call_missed" /> {{ missedRaidsText }}
  </p>
</template>

<script>
export default {
  name: 'missed-raids',
  props: ['missedRaids'],
  computed: {
    missedRaidsText () {
      let message = this.missedRaids + ' missed raid'
      if (this.missedRaids !== 1) {
        message += 's'
      }

      return message
    }
  }
}
</script>
