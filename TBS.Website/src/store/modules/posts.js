import axios from '@/axios.js'

const posts = {
  namespaced: true,
  state: {},
  getters: {},
  mutations: {},
  actions: {
    getPosts({ rootGetters, commit }, payload) {            
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        const apiCall = () => {
          let oppositeAccountType = rootGetters['authentication/getAccountType'].toLowerCase() == 'carrier' ? 'shipper' : 'carrier';
          axios({
            method: 'GET',
            url: `posts/${oppositeAccountType}/${payload.currentPage}/${payload.pageSize || 10}`,
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
    getMyPosts({ commit, rootGetters }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        axios({
          method: 'GET',
          url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}/${rootGetters['authentication/getFirebaseUser'].uid}/${payload.currentPage}/${payload.count}`,
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
    getPostById({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'GET',
          url: `posts/${payload.type || rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.postId}`,
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
    searchPosts({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      let oppositeAccountType = rootGetters['authentication/getAccountType'].toLowerCase() == 'carrier' ? 'shipper' : 'carrier';
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `posts/${oppositeAccountType}/${payload.currentPage}/${payload.pageSize}/Search`,
          data: payload.SearchModel,
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
    createPost({ commit, rootGetters}, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}`,
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
    updatePost({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'POST',
          url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.id}`,
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
    deletePost({ commit, rootGetters }, payload) {
      commit('global/setLoading', true, { root: true })
      return new Promise((resolve, reject) => {
        axios({
          method: 'DELETE',
          url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.id}`,
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

export default posts