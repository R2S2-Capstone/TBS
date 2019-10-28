import Vue from 'vue'
import Vuex from 'vuex'

import authentication from '@/store/modules/authentication.js'
import bids from '@/store/modules/bids.js'
import global from '@/store/modules/global.js'
import posts from '@/store/modules/posts.js'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    authentication,
    bids,
    global,
    posts,
    profiles,
  },
})
