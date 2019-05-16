const global = {
    namespaced: true,
    state: {
        loading: false,
    },
    getters: {
        // LOADING
        isLoading: state => state.loading,
    },
    mutations: {
        // LOADING
        setLoading: (state, isLoading) => state.loading = isLoading,
    },
    actions: {
        // LOADING
        updateLoading({ commit }, isLoading) {
            commit("setLoading", isLoading)
        },
    }
}

export default global