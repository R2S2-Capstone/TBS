import Vue from 'vue'
import Router from 'vue-router'

const Home = () => import('@/views/Home.vue')

const Login = () => import('@/views/Login.vue')
const Register = () => import('@/views/Register.vue')
const ResetPassword = () => import('@/views/ResetPassword.vue')

// Dealer
const DealerIndex = () => import('@/views/Dealer/Index.vue')
const DealerHome = () => import('@/views/Dealer/Home.vue')
const DealerCreatePost = () => import('@/views/Dealer/CreatePost.vue')

// Transporter
const TransporterIndex = () => import('@/views/Transporter/Index.vue')
const TransporterHome = () => import('@/views/Transporter/Home.vue')
const TransporterCreatePost = () => import('@/views/Transporter/CreatePost.vue')

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
      path: '/ResetPassword/:token?',
      name: 'resetPassword',
      component: ResetPassword
    },
    {
      path: '/Dealer',
      component: DealerIndex,
      // TODO: Route protection
      children: [
        {
          path: '',
          name: 'dealer',
          component: DealerHome
        },
        {
          path: 'CreatePost',
          name: 'dealerCreatePost',
          component: DealerCreatePost
        }
      ]
    },
    {
      path: '/Transporter',
      component: TransporterIndex,
      // TODO: Route protection
      children: [
        {
          path: '',
          name: 'transporter',
          component: TransporterHome
        },
        {
          path: 'createPost',
          name: 'transporterCreatePost',
          component: TransporterCreatePost
        }
      ]
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
    {
      // This will match all other routes (404)
      path: '*',
      component: Home
    }
  ]
})
