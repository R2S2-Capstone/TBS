import firebase from '@/firebase/Config.js'
import axios from '@/axios.js'
import router from '@/router.js'

const global = {
  namespaced: true,
  state: {
    email: '',
    token: null,
    accountType: '',
    refreshing: false,
    uid: null,
  },
  getters: {
    getToken: state => state.token,
    isAuthenticated: state => state.token,
    getAccountType: state => state.accountType,
    isRefreshing: state => state.refreshing,
    getFirebaseUserId: state => state.uid, 
  },
  mutations: {
    authenticate(state, data) {
      state.email = data.email
      state.token = data._lat
      state.uid = data.uid
    },
    logout(state) {
      state.email = null
      state.token = null
      state.accountType = ''
      state.uid = null
      firebase.auth().signOut()
        .then(() => {
        })
    },
    refresh(state, refreshing) {
      state.refreshing = refreshing
    },
    setAccountType(state, data) {
      if (data.data.result.accountType == 0) {
          state.accountType = "Carrier"
      } else if (data.data.result.accountType == 1) {
          state.accountType = "Shipper"
      } else {
          state.accountType = "Administrator"
      }
    }
  },
  actions: {
    register({ commit }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        firebase.auth().createUserWithEmailAndPassword(payload.email, payload.password)
          .then(() => {
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
    login({ rootGetters, commit }, payload) {
      return new Promise((resolve, reject) => {
        commit('global/setLoading', true, { root: true })
        commit('authentication/refresh', true, { root: true })
        firebase.auth().signInWithEmailAndPassword(payload.email, payload.password)
          .then((response) => {
            if (!response.user.emailVerified) {
              firebase.auth().currentUser.sendEmailVerification()
              reject("Email not verified")
              return
            }
            resolve()
          })
          .catch((error) => {
            if (rootGetters['authetncation/getToken'] != null) { // this shouldn't be the case but just in case
              commit('logout')
            }
            commit('global/setLoading', false, { root: true })
            commit('authentication/refresh', false, { root: true })
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
    refreshToken({ commit }) {
      return new Promise((resolve, reject) => {
        commit('authentication/refresh', true, { root: true })
        firebase.auth().onAuthStateChanged(user => {
          if (user) {
            if (!user.emailVerified) {
              commit('logout')
            } else {
              firebase.auth().currentUser.getIdToken()
                .then(() => {
                  commit("authenticate", user)
                  commit('global/setLoading', true, { root: true })
                  axios({
                    method: 'post',
                    url: 'authentication/login',
                    data: { userFirebaseId: user.uid },
                    headers: { Authorization: `Bearer ${user._lat}`}
                  })
                  .then((data) => {
                    commit("setAccountType", data)
                    resolve()
                  })
                  .catch((error) => {
                    commit('logout')
                    router.push({ name: 'login', params: { backendError: true } })
                    reject(error)
                  })
                  .finally(() => {
                    commit('authentication/refresh', false, { root: true })
                    commit('global/setLoading', false, { root: true })
                  })
                })
                .catch((error) => {
                  commit('global/setLoading', false, { root: true })
                  commit('authentication/refresh', false, { root: true })
                  reject(error)
                })
            }
          } else {
            commit('global/setLoading', false, { root: true })
            commit('authentication/refresh', false, { root: true })
          }
        })
      })
    }
  }
}

export default global