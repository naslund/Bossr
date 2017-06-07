<template>
  <div class="raids">
    <div class="columns is-multiline">
      <div class="column is-one-third" v-for="raid in raids">
        <div class="box">
          <p>{{ raid.frequencyHoursMin }} - {{ raid.frequencyHoursMax }} hours</p>
          <div v-for="spawn in raid.spawns" :key="spawn.id">
            <p>{{ spawn.creature.name }}</p>
            <p v-for="position in spawn.positions" :key="position.id">
              {{ position.name }}
              <b-icon icon="place"></b-icon>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'raids',
  data () {
    return {
      raids: []
    }
  },
  created () {
    this.$http.get('http://localhost:5000/api/raids').then(response => {
      this.raids = response.body
    })
  }
}
</script>
