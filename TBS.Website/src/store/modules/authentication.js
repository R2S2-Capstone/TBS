import firebase from '@/firebase/Config.js'
import axios from '@/axios.js'

const global = {
    namespaced: true,
    state: {
        email: '',
        token: '',
        refreshing: false,
        isLoggingIn: false,
    },
    getters: {
        getToken: state => state.token,
        isAuthenticated: state => state.token,
        isRefreshing: state => state.refreshing,
        isLoggingIn: state => state.isLoggingIn,
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
        },
        setIsLoggingIn: (state, isLoggingIn) => state.isLoggingIn = isLoggingIn,
    },
    actions: {
        register({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().createUserWithEmailAndPassword(payload.email, payload.password)
                    .then(() => {
                        console
                        var user = firebase.auth().currentUser
                        axios({
                            method: 'post',
                            url: 'authentication/register',
                            data: { userFirebaseId: user.uid, name: payload.name, email: payload.email, company: payload.company, accountType: payload.accountType, dealerNumber: payload.dealerNumber, rin: payload.rin, driversLicense: payload.driversLicense },
                        })
                        .then(() => {
                            // Email will only be sent once backend registration is complete
                            user.sendEmailVerification()
                            resolve()
                        })
                        .catch(() => {
                            user.delete()
                            reject()
                        })
                        .finally(() => {
                            commit('global/setLoading', false, { root: true })
                        })
                    })
                    .catch((error) => {
                        commit('global/setLoading', false, { root: true })
                        reject(error)
                    })
            })
        },
        login({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                commit('setIsLoggingIn', true)
                firebase.auth().signInWithEmailAndPassword(payload.email, payload.password)
                    .then((response) => {
                        if (!response.user.emailVerified) {
                            firebase.auth().currentUser.sendEmailVerification()
                            reject("Email not verified")
                            return
                        }
                        axios({
                            method: 'post',
                            url: 'authentication/login',
                            data: { userFirebaseId: firebase.auth().currentUser.uid },
                            headers: { Authorization: `Bearer ${response.user._lat}`}
                        })
                        .then(() => {
                            commit("authenticate", response.user)
                            resolve(response)
                        })
                        .catch((error) => {
                            commit('logout')
                            reject(error)
                        })
                        .finally(() => {
                            commit('global/setLoading', false, { root: true })
                            commit('setIsLoggingIn', false)
                        })
                    })
                    .catch((error) => {
                        commit('global/setLoading', false, { root: true })
                        commit('setIsLoggingIn', false)
                        commit('logout')
                        reject(error)
                    })
            })
        },
        resetPassword({ commit }, payload) {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                firebase.auth().sendPasswordResetEmail(payload.email)
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        refreshToken({ commit, rootGetters }) {
            return new Promise((resolve, reject) => {
                commit('authentication/refresh', true, { root: true })
                firebase.auth().onAuthStateChanged(user => {
                    if (user) {
                        if (!user.emailVerified) {
                            commit('logout')
                        } else {
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
                                    if (!(rootGetters['authentication/isLoggingIn'])) {
                                        commit('authentication/refresh', false, { root: true })
                                    }
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