import axios from '@/axios.js'

const profiles = {
  namespaced: true,
  state: {},
  getters: {},
  mutations: {},
  actions: {
    getMyProfile({ commit, rootGetters }) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        axios({
          method: 'GET',
          url: `profiles/${rootGetters['authentication/getAccountType'].toLowerCase()}`,
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
    getProfileById({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'GET',
          url: `profiles/${payload.type || rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.profileId}`,
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
    updateProfile({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `profiles/${rootGetters['authentication/getAccountType'].toLowerCase()}`,
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
  }
}

export default profiles