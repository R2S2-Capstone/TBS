import Vue from 'vue'
import Router from 'vue-router'

const Home = () => import('@/views/Home.vue')

const Login = () => import('@/views/Login.vue')
const Register = () => import('@/views/Register.vue')
const ResetPassword = () => import('@/views/ResetPassword.vue')

// General
const ViewPosts = () => import('@/views/ViewPosts.vue')

// Dealer
const DealerIndex = () => import('@/views/Dealer/Index.vue')
const DealerHome = () => import('@/views/Dealer/Home.vue')
const DealerModifyPost = () => import('@/views/Dealer/Modify.vue')
const DealerManageBids = () => import('@/views/Dealer/Bids.vue')

// Transporter
const TransporterIndex = () => import('@/views/Transporter/Index.vue')
const TransporterHome = () => import('@/views/Transporter/Home.vue')
const TransporterModifyPost = () => import('@/views/Transporter/Modify.vue')
const TransporterManageBids = () => import('@/views/Transporter/Bids.vue')
const TransporterViewBidDetails = () => import('@/views/Transporter/BidDetails.vue')

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
          name: 'dealerHome',
          component: DealerHome
        },
        {
          path: 'CreatePost',
          name: 'dealerCreatePost',
          component: DealerModifyPost
        },
        {
          path: 'EditPost/:id?',
          name: 'dealerEditPost',
          component: DealerModifyPost
        },
        {
          path: 'ManageBids/:id?',
          name: 'dealerManageBids',
          component: DealerManageBids
        },
      ]
    },
    {
      path: '/Transporter',
      component: TransporterIndex,
      // TODO: Route protection
      children: [
        {
          path: '',
          name: 'transporterHome',
          component: TransporterHome
        },
        {
          path: 'CreatePost',
          name: 'transporterCreatePost',
          component: TransporterModifyPost
        },
        {
          path: 'EditPost/:id?',
          name: 'transporterEditPost',
          component: TransporterModifyPost
        },
        {
          path: 'ManageBids/:id?',
          name: 'transporterManageBids',
          component: TransporterManageBids
        },
        {
          path: 'ViewBid/:id?',
          name: 'transporterViewBidDetails',
          component: TransporterViewBidDetails
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
