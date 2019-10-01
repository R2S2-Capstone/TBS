import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'

const Index = () => import('@/views/Index.vue')
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
const ShipperEditProfile = () => import('@/views/Shipper/Me.vue')

// Carrier
const CarrierIndex = () => import('@/views/Carrier/Index.vue')
const CarrierHome = () => import('@/views/Carrier/Home.vue')
const CarrierModifyPost = () => import('@/views/Carrier/Modify.vue')
const CarrierManageBids = () => import('@/views/Carrier/Bids.vue')
const CarrierViewBidDetails = () => import('@/views/Carrier/BidDetails.vue')
const CarrierEditProfile = () => import('@/views/Carrier/Me.vue')

// Posts
const PostsIndex = () => import('@/views/Posts/Index.vue')
const AllPosts = () => import('@/views/Posts/All.vue')
// Shipper Posts
const DetailedShipperPost = () => import('@/views/Posts/Shipper/Details.vue')
// Carrier Posts
const DetailedCarrierPost = () => import('@/views/Posts/Carrier/Details.vue')

// Delivery
const DeliveryIndex = () => import('@/views/Delivery/Index.vue')
const DeliveryHome = () => import('@/views/Delivery/Home.vue')
const CarrierDelivery = () => import('@/views/Delivery/Carrier/Index.vue')
const ShipperDelivery = () => import('@/views/Delivery/Shipper/Index.vue')

// About
const AboutIndex = () => import('@/views/About/Index.vue')
const AboutUs = () => import('@/views/About/Us.vue')
const PrivacyPolicy = () => import('@/views/About/PrivacyPolicy.vue')
const FAQ = () => import('@/views/About/FAQ.vue')
const AboutCarrier = () => import('@/views/About/Carrier.vue')
const AboutShipper = () => import('@/views/About/Shipper.vue')

//Profile
const ProfileIndex = () => import('@/views/Profile/Index.vue')
const CarrierProfile = () => import('@/views/Profile/Carrier.vue')
const ShipperProfile = () => import('@/views/Profile/Shipper.vue')

Vue.use(Router)

// Add when it is required to be logged in to view this page
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

// Add when it is required to be logged out to view this page
const NotLoggedIn = {
  beforeEnter: (to, from, next) => {
    const redirect = () => {
      const token = store.getters['authentication/getToken']
      if (!token) {
        next()
      } else {
        next(from)
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

// This will redirect to either the home page or view posts depending on if the user is logged in or not
const CustomHomePage = {
  beforeEnter: (to, from, next) => {
    const redirect = () => {
      const token = store.getters['authentication/getToken']
      if (token) {
        next({ name: 'viewAllPosts' })
      } else {
        next({ name: 'homepage' })
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

// Add when only a carrier can view this page
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

// Add when only shipper can view this page
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
      // Route does not contain any HTML, it is simply used as a redirect for the homepage
      // depending on if the user is logged in or not
      path: '/',
      name: 'home',
      component: Index,
      ...CustomHomePage
    },
    {
      // Default route for anyone who is not logged in, it contains a lot of information
      // regarding the website
      path: '/home',
      name: 'homepage',
      component: Home,
      ...NotLoggedIn
    },
    {
      path: '/About',
      component: AboutIndex,
      children: [
        {
          path: '',
          name: 'aboutUs',
          component: AboutUs,
        },
        {
          path: 'Carrier',
          name: 'aboutCarrier',
          component: AboutCarrier,
        },
        {
          path: 'Shipper',
          name: 'aboutShipper',
          component: AboutShipper,
        },
        {
          path: 'FAQ',
          name: 'faq',
          component: FAQ,
        },
        {
          path: 'PrivacyPolicy',
          name: 'privacyPolicy',
          component: PrivacyPolicy,
        },
      ]
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
      path: '/ResetPassword/',
      name: 'resetPassword',
      component: ResetPassword
    },
    {
      path: '/Profile',
      component: ProfileIndex,
      children: [
        {
          path: 'Carrier/:id?',
          name: 'carrierProfile',
          component: CarrierProfile,
          props: true,
        },
        {
          path: 'Shipper/:id?',
          name: 'shipperProfile',
          component: ShipperProfile,
          props: true,
        },
      ]
    },
    {
      // All routes for shipper only pages
      path: '/Shipper',
      component: ShipperIndex,
      ...ShipperOnly,
      children: [
        {
          // Default shipper page, also known as 'dashboard' and 'manage my posts'
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
          path: 'Me',
          name: 'shipperEditProfile',
          component: ShipperEditProfile,
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
          props: true,
          ...LoggedIn
        },
      ]
    },
    {
      // All routes for carrier only pages
      path: '/Carrier',
      component: CarrierIndex,
      ...CarrierOnly,
      children: [
        {
          // Default carrier page, also known as 'dashboard' and 'manage my posts'
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
          path: 'Me',
          name: 'carrierEditProfile',
          component: CarrierEditProfile,
          ...LoggedIn
        },
        {
          path: 'EditPost/:id?',
          name: 'carrierEditPost',
          component: CarrierModifyPost,
          props: true,
          ...LoggedIn
        },
        {
          path: 'Bids/:id?',
          name: 'carrierManageBids',
          component: CarrierManageBids,
          props: true,
          ...LoggedIn
        },
        {
          path: 'ViewBid/:id?',
          name: 'carrierViewBidDetails',
          component: CarrierViewBidDetails,
          props: true,
          ...LoggedIn,
        },
      ]
    },
    {
      // All routes for viewing others posts
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
      // All routes for viewing delivery information
      path: '/Delivery',
      component: DeliveryIndex,
      children: [
        {
          path: '',
          name: 'delivery',
          component: DeliveryHome,
        },
        {
          path: 'Carrier/:postId?/:bidId?',
          name: 'carrierDelivery',
          component: CarrierDelivery,
          props: true,
          ...LoggedIn
        },
        {
          path: 'Shipper/:postId?/:bidId?',
          name: 'shipperDelivery',
          component: ShipperDelivery,
          props: true,
          ...LoggedIn
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
