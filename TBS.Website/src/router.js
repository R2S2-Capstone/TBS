import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'

const Home = () => import('@/views/Home.vue')
const Error401 = () => import('@/views/Error/401.vue')
const Error404 = () => import('@/views/Error/404.vue')

const Login = () => import('@/views/Login.vue')
const Register = () => import('@/views/Register.vue')
const ResetPassword = () => import('@/views/ResetPassword.vue')

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

// Posts
const PostsIndex = () => import('@/views/Posts/Index.vue')
const AllPosts = () => import('@/views/Posts/All.vue')
// Shipper Posts
const DetailedShipperPost = () => import('@/views/Posts/Shipper/Details.vue')
// Carrier Posts
const DetailedCarrierPost = () => import('@/views/Posts/Carrier/Details.vue')

// About
const Us = () => import('@/views/About/Us.vue')

// Privacy
const Privacy = () => import('@/views/About/Privacy.vue')

// FAQ
const FAQ = () => import('@/views/About/FAQ.vue')

// Carrier
const Carrier = () => import('@/views/About/Carrier.vue')

// Shipper
const Shipper = () => import('@/views/About/Shipper.vue')

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

const CarrierOnly = {
  beforeEnter: (to, from, next) => {
    const redirect = () => {
      const accountType = store.getters['authentication/getAccountType']
      // We do this because we want all other account types (Carrier and administrator) to access it
      if (accountType == "Shipper") {
        next({ name: 'error401' })
      } else {
        next()
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

const ShipperOnly = {
  beforeEnter: (to, from, next) => {
    const redirect = () => {
      const accountType = store.getters['authentication/getAccountType']
      // We do this because we want all other account types (Shipper and administrator) to access it
      if (accountType == "Carrier") {
        next({ name: 'error401' })
      } else {
        next()
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
      path: '/About/Us',
      name: 'aboutUs',
      component: Us
    },
    {
      path: '/About/Carrier',
      name: 'carrier',
      component: Carrier
    },
    {
      path: '/About/Privacy',
      name: 'privacy',
      component: Privacy
    },
    {
      path: '/About/FAQ',
      name: 'faq',
      component: FAQ
    },
    {
      path: '/About/Shipper',
      name: 'shipper',
      component: Shipper
    },
    {
      path: '/Login',
      name: 'login',
      component: Login,
      props: true,
    },
    {
      path: '/Register',
      name: 'register',
      component: Register,
    },
    {
      path: '/ResetPassword/',
      name: 'resetPassword',
      component: ResetPassword
    },
    {
      path: '/Shipper',
      component: ShipperIndex,
      ...ShipperOnly,
      children: [
        {
          path: '',
          name: 'shipperHome',
          component: ShipperHome,
          ...LoggedIn
        },
        {
          path: 'CreatePost',
          name: 'shipperCreatePost',
          component: ShipperModifyPost,
          ...LoggedIn
        },
        {
          path: 'EditPost/:id?',
          name: 'shipperEditPost',
          component: ShipperModifyPost,
          ...LoggedIn
        },
        {
          path: 'Bids/:id?',
          name: 'shipperManageBids',
          component: ShipperManageBids,
          ...LoggedIn
        },
      ]
    },
    {
      path: '/Carrier',
      component: CarrierIndex,
      ...CarrierOnly,
      children: [
        {
          path: '',
          name: 'carrierHome',
          component: CarrierHome,
          ...LoggedIn
        },
        {
          path: 'CreatePost',
          name: 'carrierCreatePost',
          component: CarrierModifyPost,
          ...LoggedIn
        },
        {
          path: 'EditPost/:id?',
          name: 'carrierEditPost',
          component: CarrierModifyPost,
          ...LoggedIn
        },
        {
          path: 'Bids/:id?',
          name: 'carrierManageBids',
          component: CarrierManageBids,
          ...LoggedIn
        },
        {
          path: 'ViewBid/:id?',
          name: 'carrierViewBidDetails',
          component: CarrierViewBidDetails,
          ...LoggedIn,
          params: true,
        },
      ]
    },
    {
      path: '/Posts',
      component: PostsIndex,
      children: [
        {
          path: '',
          name: 'viewAllPosts',
          component: AllPosts,
        },
        {
          path: 'Carrier/Details/:id?',
          name: 'viewDetailedCarrierPost',
          component: DetailedCarrierPost,
          props: true,
        },
        {
          path: 'Shipper/Details/:id?',
          name: 'viewDetailedShipperPost',
          component: DetailedShipperPost,
          props: true,
        },
      ]
    },
    {
      path: '/401',
      name: 'error401',
      component: Error401
    },
    {
      // This will match all other routes (404)
      path: '*',
      component: Error404
    }
  ]
})
