import firebase from '@/firebase/Config.js'
import axios from '@/axios.js'

const global = {
    namespaced: true,
    state: {
        email: '',
        token: '',
        refreshing: false,
    },
    getters: {
        getToken: state => state.token,
        isAuthenticated: state => state.token,
        isRefreshing: state => state.refreshing,
    },
    mutations: {
        authenticate(state, userData) {
            state.email = userData.email
            state.token = userData._lat
        },
        logout(state) {
            state.email = null
            state.token = null
            firebase.auth().signOut()
        },
        refresh(state, refreshing) {
            state.refreshing = refreshing
        }
    },
    actions: {
        register({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().createUserWithEmailAndPassword(payload.email, payload.password)
                    .then(() => {
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
        login({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().signInWithEmailAndPassword(payload.email, payload.password)
                    .then((response) => {
                        if (!response.user.emailVerified) {
                            firebase.auth().currentUser.sendEmailVerification()
                            reject("Email not verified")
                        }
                        console.log(firebase.auth().currentUser.uid);
                        axios({
                            method: 'post',
                            url: 'authentication/login',
                            data: { firebaseUserId: firebase.auth().currentUser.uid },
                            headers: { Authorization: `Bearer ${response.user._lat}`}
                        }).then(() => {
                            commit("authenticate", response.user)
                            resolve(response)
                        })
                        .catch((error) => {
                            reject(error)
                        })
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
                commit('authentication/refresh', true, { root: true })
                firebase.auth().onAuthStateChanged(user => {
                    if (user) {
                        if (!user.emailVerified) {
                            commit('logout')
                        } else {
                            commit('global/setLoading', true, { root: true })
                            firebase.auth().currentUser.getIdToken()
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
                                    commit('authentication/refresh', false, { root: true })
                                    commit('global/setLoading', false, { root: true })
                                })
                            }
                        }
                        else {
                            commit('authentication/refresh', false, { root: true })
                        }
                    })
            })
        }
    }
}

export default global