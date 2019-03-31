import Vue from 'vue'
import Vuex from 'vuex'

import global from '@/store/modules/global.js'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    global
  },
})
