const posts = {
    namespaced: true,
    state: {},
    getters: {},
    mutations: {},
    actions: {
        getPosts() {

        },
        getMyPosts({ rootGetters,}, payload) {
            axios({
                method: 'post',
                url: `carrier/${rootGetters['authentication/firebaseId']}/Posts/${payload.id}`,
                data: { userFirebaseId: user.uid },
                headers: { Authorization: `Bearer ${user._lat}`}
            })
            .then((data) => {
                commit("setAccountType", data)
                resolve()
            })
            .catch((error) => {
                commit('logout')
                reject(error)
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