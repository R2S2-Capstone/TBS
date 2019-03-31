import firebase from '@/firebase/Config.js'
const global = {
    namespaced: true,
    state: {
        email: '',
        token: '',
    },
    getters: {
        isAuthenticated: state => state.token
    },
    mutations: {
        authenticate(state, userData) {
            state.email = userData.email
            state.token = userData._lat
        },
        logout(state) {
            state.email = null
            firebase.auth().signOut()
        },
    },
    actions: {
        register({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().createUserWithEmailAndPassword(payload.email, payload.password)
                    .then(() => {
                        // commit("authenticate", response.user)
                        resolve()
                    })
                    .catch((error) => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        signin({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().signInWithEmailAndPassword(payload.email, payload.password)
                    .then((response) => {
                        commit("authenticate", response.user)
                        resolve(response)
                    })
                    .catch((error) => {
                        reject(error)
                    }).finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        refreshToken({ commit }) {
            return new Promise((resolve, reject) => {
                firebase.auth().onAuthStateChanged(user => {
                    if (user) {
                        commit('global/setLoading', true, { root: true })
                        firebase.auth().currentUser.getIdToken(true)
                            .then((response) => {
                                commit("authenticate", {
                                    _lat: response,
                                    email: firebase.auth().currentUser.email
                                })
                                resolve()
                            })
                            .catch((error) => {
                                reject(error)
                            })
                            .finally(() => {
                                commit('global/setLoading', false, { root: true })
                            })
                    }
                })
            })
        }
    }
}

export default global