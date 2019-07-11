import axios from '@/axios.js'

const posts = {
    namespaced: true,
    state: {},
    getters: {},
    mutations: {},
    actions: {
        getPosts() {

        },
        getMyPosts({ rootGetters,}, payload) {
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
            })
        },
        getPostById(payload) {
             
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