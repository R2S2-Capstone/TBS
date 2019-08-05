import axios from '@/axios.js'

const bids = {
  namespaced: true,
  state: {},
  getters: {},
  mutations: {},
  actions: {
    getBids({ rootGetters, commit }, payload) {            
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        const apiCall = () => {
          let oppositeAccountType = rootGetters['authentication/getAccountType'].toLowerCase() == 'carrier' ? 'shipper' : 'carrier';
          axios({
            method: 'GET',
            url: `bids/${oppositeAccountType}/${payload.currentPage}/${payload.pageSize || 10}`,
          })
          .then((response) => {
            resolve(response)
          })
          .catch((error) => {
            reject(error)
          })
          .finally(() => {
            commit('global/setLoading', false, { root: true })
          })
        }

        if (rootGetters['authentication/isRefreshing']) {
          this.watch(() => rootGetters['authentication/isRefreshing'],
            () => {
              apiCall()
            }
          )
        } else {
          apiCall()
        }
      })
    },
    getMyBids({ commit, rootGetters }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        axios({
          method: 'GET',
          url: `bids/${rootGetters['authentication/getAccountType'].toLowerCase()}/${rootGetters['authentication/getFirebaseUser'].uid}/${payload.currentPage}/${payload.count}`,
          headers: { Authorization: `Bearer ${rootGetters['authentication/getToken']}` }
        })
        .then((response) => {
          resolve(response)
        })
        .catch((error) => {
          reject(error)
        })
        .finally(() => {
          commit('global/setLoading', false, { root: true })
        })
      })
    },
    getBidsByPostId({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'GET',
          url: `bids/${payload.type || rootGetters['authentication/getAccountType'].toLowerCase()}/${rootGetters['authentication/getFirebaseUser'].uid}/${payload.postId}/${payload.currentPage}/${payload.pageSize}`,
          headers: { Authorization: `Bearer ${rootGetters['authentication/getToken']}`}
        })
        .then((response) => {
          resolve(response)
        })
        .catch((error) => {
          reject(error)
        })
        .finally(() => {
          commit('global/setLoading', false, { root: true })
        })
      })
    },
    createBid({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `bids/${rootGetters['authentication/getAccountType'].toLowerCase()}`,
          data: payload,
          headers: { Authorization: `Bearer ${rootGetters['authentication/getToken']}`}
        })
        .then((response) => {
          resolve(response)
        })
        .catch((error) => {
          reject(error)
        })
        .finally(() => {
          commit('global/setLoading', false, { root: true })
        })
      })
    },
    deleteBid({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'DELETE',
          url: `bids/${rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.id}`,
          data: payload,
          headers: { Authorization: `Bearer ${rootGetters['authentication/getToken']}`}
        })
        .then((response) => {
          resolve(response)
        })
        .catch((error) => {
          reject(error)
        })
        .finally(() => {
          commit('global/setLoading', false, { root: true })
        })
      })
    }
  }
}

export default bids