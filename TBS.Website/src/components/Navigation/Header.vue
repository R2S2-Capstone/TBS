<template>
	<nav class="navbar navbar-expand-lg navbar-light bg-white">
		<!-- <router-link :to="{ name: 'home' }" class="navbar-brand font-blue-on-hover text-white"><i class="fas fa-truck"></i> TBS </router-link> -->
		<router-link class="navbar-brand" :to="{ name: 'home' }">
			<img src="https://static.reecerose.com/images/tbs/logo.png" alt="TBS">
        </router-link>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse text-center" id="navigationBar">
			<ul class="navbar-nav mr-auto">
				<li class="nav-item">
					<router-link :to="{ name: 'home' }" class="btn font-blue-on-hover">Home</router-link>
				</li>
				<li class="nav-item">
					<router-link :to="{ name: 'viewAllPosts' }" class="btn font-blue-on-hover">View Posts</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated  && accountType != ''">
					<router-link :to="{ name: `${accountType}CreatePost` }" class="btn font-blue-on-hover">Create a Post</router-link>
				</li>
			</ul>
			<ul class="navbar-nav ml-auto">
				<li class="nav-item" v-if="!isAuthenticated">
					<router-link :to="{ name: 'login' }" class="btn font-blue-on-hover"><i class="fas fa-sign-in-alt"></i> Login</router-link>
				</li>
				<li class="nav-item" v-if="!isAuthenticated">
					<router-link :to="{ name: 'register' }" class="btn font-blue-on-hover"><i class="fas fa-sign-in-alt"></i> Register</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated && accountType != ''">
					<router-link :to="{ name: `${accountType}Home` }" class="btn font-blue-on-hover">Dashboard</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated">
					<span class="btn font-blue-on-hover pointer" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
				</li>
			</ul>
		</div>
	</nav>
</template>

<script>
export default {
	name: 'navigationHeader',
	methods: {
		logout() {
			this.$store.commit('authentication/logout')
			this.$router.push({ name: 'home' })
		},
	},
	computed: {
		isAuthenticated() {
			return this.$store.getters['authentication/isAuthenticated']
		},
		accountType() {
			return this.$store.getters['authentication/getAccountType'].toLowerCase() || ''
		}
	}
}
</script>

<style lang="scss">
nav {
	// box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 8px 0 rgba(0, 0, 0, 0.19);
	border-bottom: 1px solid rgba(0, 0, 0, 0.19);
	z-index: 2;
}
</style>
