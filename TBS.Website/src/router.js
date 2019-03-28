import Vue from 'vue'
import Router from 'vue-router'

const Home = () => import('@/views/Home.vue')

const Login = () => import('@/views/Login.vue')
const Register = () => import('@/views/Register.vue')

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/Login',
      name: 'login',
      component: Login
    },
    {
      path: '/Register',
      name: 'register',
      component: Register
    },
    {
      path: '/CreatePost',
      name: 'createPost',
      component: null
    },
    {
      path: '/ManagePosts',
      name: 'managePosts',
      component: null
    },
    {
      path: '/Browse',
      name: 'browsePosts',
      component: null
    },
  ]
})
