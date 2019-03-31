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
        register(payload) {
            return new Promise((resolve, reject) => {
                firebase.auth().createUserWithEmailAndPassword(payload.email, payload.password)
                    .then(() => {
                        // commit("authenticate", response.user)
                        resolve()
                    })
                    .catch((error) => {
                        reject(error)
                    })
            })
        },
        signin({ commit }, payload) {
            return new Promise((resolve, reject) => {
                firebase.auth().signInWithEmailAndPassword(payload.email, payload.password)
                    .then((response) => {
                        commit("authenticate", response.user)
                        resolve(response)
                    })
                    .catch((error) => {
                        reject(error)
                    })
            })
        },
        refreshToken({ commit }) {
            return new Promise((resolve, reject) => {
                firebase.auth().onAuthStateChanged(user => {
                    if (user) {
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
                    }
                })
            })
        }
    }
}

export default global