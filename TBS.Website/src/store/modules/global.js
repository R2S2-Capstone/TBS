const global = {
    namespaced: true,
    state: {
        loading: false,
    },
    getters: {
        isLoading: state => state.loading,
    },
    mutations: {
        setLoading: (state, isLoading) => state.loading = isLoading,
    },
    actions: {
    }
}

export default global