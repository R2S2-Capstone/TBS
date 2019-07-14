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
                    method: 'get',
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
                    method: 'get',
                    url: `${rootGetters['authentication/getAccountType'].toLowerCase()}/posts/all/${payload.currentPage}/${payload.count}`,
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
        getPostById({ commit, rootGetters }, payload) {
            commit('global/setLoading', true, { root: true })
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `${rootGetters['authentication/getAccountType'].toLowerCase()}/posts/${payload.postId}`,
                    data: { postId: payload.postId },
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
                    method: 'post',
                    url: `${rootGetters['authentication/getAccountType'].toLowerCase()}/posts`,
                    data: { post: payload.post },
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
                    method: 'put',
                    url: `${rootGetters['authentication/getAccountType'].toLowerCase()}/posts`,
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
                    method: 'delete',
                    url: `${rootGetters['authentication/getAccountType'].toLowerCase()}/posts`,
                    data: { postId: payload.postId },
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