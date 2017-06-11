<template>
  <div class="worlds">
    <ul>
      <li v-for="world in worlds">
        <router-link :to="{ name: 'states', params: { worldid: world.id } }">
          {{world.name}}
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'worlds',
  data () {
    return {
      worlds: []
    }
  },
  created () {
    this.$http.get(process.env.API_URL + 'api/worlds').then(response => {
      function compare (a, b) {
        if (a.name < b.name) {
          return -1
        }
        if (a.name > b.name) {
          return 1
        }
        return 0
      }

      this.worlds = response.body.sort(compare)
    })
  }
}
</script>

<style>
li {
  display: inline-block;
  margin: 0 10px;
}
</style>
