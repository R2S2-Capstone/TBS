import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'

const Home = () => import('@/views/Home.vue')

const Login = () => import('@/views/Login.vue')
const Register = () => import('@/views/Register.vue')
const ResetPassword = () => import('@/views/ResetPassword.vue')

// General
const ViewPosts = () => import('@/views/ViewPosts.vue')

// Shipper
const ShipperIndex = () => import('@/views/Shipper/Index.vue')
const ShipperHome = () => import('@/views/Shipper/Home.vue')
const ShipperModifyPost = () => import('@/views/Shipper/Modify.vue')
const ShipperManageBids = () => import('@/views/Shipper/Bids.vue')

// Carrier
const CarrierIndex = () => import('@/views/Carrier/Index.vue')
const CarrierHome = () => import('@/views/Carrier/Home.vue')
const CarrierModifyPost = () => import('@/views/Carrier/Modify.vue')
const CarrierManageBids = () => import('@/views/Carrier/Bids.vue')
const CarrierViewBidDetails = () => import('@/views/Carrier/BidDetails.vue')

Vue.use(Router)

const LoggedIn = {
  beforeEnter: (to, from, next) => {
    const redirect = () => {
      const token = store.getters['authentication/getToken']
      if (token) {
        next()
      } else {
        next({ name: 'login',  params: { redirect: to.fullPath } })
      }
    }
    if (store.getters['authentication/isRefreshing']) {
      store.watch(() => store.getters['authentication/isRefreshing'],
        () => {
          redirect()
        }
      )
    } else {
      redirect()
    }
  }
}

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
      component: Login,
    },
    {
      path: '/Register',
      name: 'register',
      component: Register,
    },
    {
      path: '/ResetPassword/:token?',
      name: 'resetPassword',
      component: ResetPassword
    },
    {
      path: '/Shipper',
      component: ShipperIndex,
      ...LoggedIn,
      // TODO: Make sure user is a shipper
      children: [
        {
          path: '',
          name: 'shipperHome',
          component: ShipperHome
        },
        {
          path: 'CreatePost',
          name: 'shipperCreatePost',
          component: ShipperModifyPost
        },
        {
          path: 'EditPost/:id?',
          name: 'shipperEditPost',
          component: ShipperModifyPost
        },
        {
          path: 'ManageBids/:id?',
          name: 'shipperManageBids',
          component: ShipperManageBids
        },
      ]
    },
    {
      path: '/Carrier',
      component: CarrierIndex,
      ...LoggedIn,
      // TODO: Make sure user is a carrier
      children: [
        {
          path: '',
          name: 'carrierHome',
          component: CarrierHome
        },
        {
          path: 'CreatePost',
          name: 'carrierCreatePost',
          component: CarrierModifyPost
        },
        {
          path: 'EditPost/:id?',
          name: 'carrierEditPost',
          component: CarrierModifyPost
        },
        {
          path: 'ManageBids/:id?',
          name: 'carrierManageBids',
          component: CarrierManageBids
        },
        {
          path: 'ViewBid/:id?',
          name: 'carrierViewBidDetails',
          component: CarrierViewBidDetails
        },
      ]
    },
    {
      path: '/ViewPosts',
      name: 'viewPosts',
      component: ViewPosts
    },
    {
      // This will match all other routes (404)
      path: '*',
      component: Home
    }
  ]
})
