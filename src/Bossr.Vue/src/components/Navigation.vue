<template>
  <nav class="nav has-shadow" id="navbar">
    <div class="container">
      <div class="nav-left">
        <a class="nav-item">
          <img src="../assets/bossr-text-icon.png" alt="Bossr Logo">
        </a>
        <router-link to="/" class="nav-item is-tab is-hidden-mobile" active-class="is-active" exact>Dashboard</router-link>
        <router-link to="/worlds" class="nav-item is-tab is-hidden-mobile" active-class="is-active">Worlds</router-link>
      </div>

      <!-- This "nav-toggle" hamburger menu is only visible on mobile -->
      <!-- You need JavaScript to toggle the "is-active" class on "nav-menu" -->
      <span class="nav-toggle">
        <span></span>
        <span></span>
        <span></span>
      </span>

      <!-- This "nav-menu" is hidden on mobile -->
      <!-- Add the modifier "is-active" to display it on mobile -->
      <div class="nav-right nav-menu">
        <div class="nav-item">
          <a class="button is-danger" v-if="isLoggedIn" v-on:click="logout">Logout</a>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  computed: {
    isLoggedIn () {
      return this.$store.state.auth.isLoggedIn
    }
  },
  methods: {
    logout () {
      this.$auth.logout()
    }
  }
}
</script>
