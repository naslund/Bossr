import Vue from 'vue'
import Router from 'vue-router'
import Worlds from '@/components/Worlds'
import World from '@/components/World'
import Login from '@/components/Login'
import Dashboard from '@/components/Dashboard'
import Raids from '@/components/Raids'

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: Dashboard,
      beforeEnter: guardRoute
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
      beforeEnter: loginGuardRoute
    },
    {
      path: '/world/:id',
      name: 'world',
      component: World,
      beforeEnter: guardRoute
    },
    {
      path: '/worlds',
      name: 'worlds',
      component: Worlds,
      beforeEnter: guardRoute
    },
    {
      path: '/raids',
      name: 'raids',
      component: Raids,
      beforeEnter: guardRoute
    }
  ]
})

function loginGuardRoute (to, from, next) {
  const auth = router.app.$options.store.state.auth

  if (auth.isLoggedIn) {
    next({ path: '/' })
  } else {
    next()
  }
}

function guardRoute (to, from, next) {
  const auth = router.app.$options.store.state.auth

  if (!auth.isLoggedIn) {
    next({ path: '/login' })
  } else {
    next()
  }
}

export default router
