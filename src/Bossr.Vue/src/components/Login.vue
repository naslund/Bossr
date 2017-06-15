<template>
  <section class="section">
    <div class="container">
      <div class="box">
        <b-field>
          <b-input v-model="credentials.username" placeholder="Username" expanded></b-input>
        </b-field>
        <b-field>
          <b-input v-model="credentials.password" placeholder="Password" type="password" password-reveal expanded></b-input>
        </b-field>
        <b-field>
          <a class="button is-success is-fullwidth" v-on:click="login">Login</a>
        </b-field>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  data () {
    return {
      credentials: {
        username: '',
        password: ''
      }
    }
  },
  methods: {
    login () {
      this.$auth.login(this.credentials, 'dashboard')
    }
  }
}
</script>
