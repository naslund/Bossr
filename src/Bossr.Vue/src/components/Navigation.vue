<template>
  <nav class="nav has-shadow" id="navbar">
    <div class="container">
      <div class="nav-left">
        <a class="nav-item">
          <router-link to="/" class="nav-item" active-class="is-active" exact>
            <img src="../assets/bossr-text-icon.png" alt="Bossr Logo">
          </router-link>
        </a>
      </div>

      <!-- This "nav-toggle" hamburger menu is only visible on mobile -->
      <!-- You need JavaScript to toggle the "is-active" class on "nav-menu" -->
      <span v-on:click="toggleHamburgerMenu" class="nav-toggle">
        <span></span>
        <span></span>
        <span></span>
      </span>

      <!-- This "nav-menu" is hidden on mobile -->
      <!-- Add the modifier "is-active" to display it on mobile -->
      <div v-bind:class="{ 'is-active': isHamburgerMenuActive }" class="nav-right nav-menu">
        <router-link to="/worlds" class="nav-item" active-class="is-active">Worlds</router-link>
        <div class="nav-item">
          <a class="button is-danger" v-if="isLoggedIn" v-on:click="logout">Logout</a>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  data () {
    return {
      isHamburgerMenuActive: false
    }
  },
  computed: {
    isLoggedIn () {
      return this.$store.state.auth.isLoggedIn
    }
  },
  methods: {
    logout () {
      this.$auth.logout()
    },
    toggleHamburgerMenu () {
      this.isHamburgerMenuActive = !this.isHamburgerMenuActive
    }
  }
}
</script>
