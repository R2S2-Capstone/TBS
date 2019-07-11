import axios from '@/axios.js'

const posts = {
    namespaced: true,
    state: {},
    getters: {},
    mutations: {},
    actions: {
        getPosts() {

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
        getPostById({ rootGetters }, payload) {
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
        createPost() {

        },
        updatePost() {

        },
        deletePost() {

        }
    }
}

export default posts