<template>
  <div class="raids">
    <div class="columns is-multiline">
      <div class="column is-one-third" v-for="state in states">
        <div class="box">
          <p>{{ state.raid.frequencyHoursMin }} - {{ state.raid.frequencyHoursMax }} hours</p>
          <div v-for="spawn in state.raid.spawns" :key="spawn.id">
            <p>{{ spawn.creature.name }}</p>
            <p v-for="position in spawn.positions" :key="position.id">
              {{ position.name }}
              <b-icon icon="place"></b-icon>
            </p>
          </div>
          <hr>
          <p>Occurs between:</p>
          <p>{{ getFormattedTime(state.expectedMin) }} - {{ getFormattedTime(state.expectedMax) }}</p>
          <hr>
          <p>Missed: {{ state.missedRaids }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'

export default {
  data () {
    return {
      states: []
    }
  },
  created () {
    this.$http.get('http://localhost:5000/api/states/' + this.$route.params.id).then(response => {
      console.log(response.body)
      this.states = response.body
    })
  },
  methods: {
    getFormattedTime (time) {
      return moment(time).fromNow()
    }
  }
}
</script>

<style>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}
</style>
