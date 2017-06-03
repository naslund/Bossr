<template>
  <div id="app">
    <navigation v-if="isLoggedIn"></navigation>
    <section v-if="!isLoggedIn" class="section">
      <div class="container" id="big-logo">
        <img src="./assets/bossr-text-icon.png" alt="Bossr Logo">
      </div>
    </section>
    <section class="section">
      <div class="container">
        <router-view></router-view>
      </div>
    </section>
  </div>
</template>

<script>
import Navigation from './components/Navigation'

export default {
  name: 'app',
  components: { Navigation },
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

<style lang="scss">
// Import Bulma's core
@import "~bulma/sass/utilities/_all";

// Set your colors
$primary: #8BC34A;
$primary-invert: findColorInvert($primary);
$twitter: #4099FF;
$twitter-invert: findColorInvert($twitter);

// Setup $colors to use as bulma classes (e.g. 'is-twitter')
$colors: (
    "white": ($white, $black),
    "black": ($black, $white),
    "light": ($light, $light-invert),
    "dark": ($dark, $dark-invert),
    "primary": ($primary, $primary-invert),
    "info": ($info, $info-invert),
    "success": ($success, $success-invert),
    "warning": ($warning, $warning-invert),
    "danger": ($danger, $danger-invert),
    "twitter": ($twitter, $twitter-invert)
);

// Links
$link: $primary;
$link-invert: $primary-invert;
$link-focus-border: $primary;

// Import Bulma and Buefy styles
@import "~bulma";
@import "~buefy/src/scss/buefy";

#big-logo {
  max-width: 500px;
  margin: 0 auto;
}
</style>
