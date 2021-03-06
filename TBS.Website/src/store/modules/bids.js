import axios from '@/axios.js'

const bids = {
  namespaced: true,
  state: {},
  getters: {},
  mutations: {},
  actions: {
    getBidById({ commit, rootGetters }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        axios({
          method: 'GET',
          url: `bids/${payload.type}/${payload.bidId}`,
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
    getMyBids({ commit, rootGetters }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        axios({
          method: 'GET',
          url: `bids/${payload.type}/${rootGetters['authentication/getFirebaseUserId']}/${payload.currentPage}/${payload.pageSize}`,
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
    createBid({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `bids/${payload.type}`,
          data: { postId: payload.postId, bid: payload.bid },
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
    updateBid({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'PUT',
          url: `bids/${payload.type}`,
          data: { bidId: payload.bidId, status: payload.bidStatus },
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
    createReview({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `reviews/${payload.type}`,
          data: payload.Review,
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
    sendReminder({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'GET',
          url: `bids/${payload.type}/${payload.bidId}/SendReminder`,
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
  }
}

export default bids