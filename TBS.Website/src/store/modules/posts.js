import axios from '@/axios.js'

const posts = {
    namespaced: true,
    state: {},
    getters: {},
    mutations: {},
    actions: {
        getPosts({ commit }, payload) {            
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'GET',
                    url: `posts/${payload.postType}`,
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
        getMyPosts({ commit, rootGetters }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'GET',
                    url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.currentPage}/${payload.count}`,
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
                    url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}/${payload.postId}`,
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
                    method: 'PUT',
                    url: `posts/${rootGetters['authentication/getAccountType'].toLowerCase()}`,
                    data: { postId: payload.postId, post: payload.post },
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
        }
    }
}

export default posts