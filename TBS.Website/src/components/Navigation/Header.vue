<template>
	<nav class="navbar navbar-expand-lg navbar-light bg-blue">
		<router-link :to="{ name: 'home' }" class="navbar-brand fade-on-hover text-white"><i class="fas fa-truck"></i> TBS </router-link>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse text-center" id="navigationBar">
			<ul class="navbar-nav mr-auto">
				<li class="nav-item">
					<router-link :to="{ name: 'home' }" class="btn text-white fade-on-hover">Home</router-link>
				</li>
				<li class="nav-item">
					<router-link :to="{ name: 'viewPosts' }" class="btn text-white fade-on-hover">View Posts</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated">
					<router-link :to="{ name: 'transporterCreatePost' }" class="btn text-white fade-on-hover">Create a Post</router-link>
				</li>
			</ul>
			<ul class="navbar-nav ml-auto">
				<li class="nav-item" v-if="!isAuthenticated">
					<router-link :to="{ name: 'login' }" class="btn text-white fade-on-hover"><i class="fas fa-sign-in-alt"></i> Login</router-link>
				</li>
				<li class="nav-item" v-if="!isAuthenticated">
					<router-link :to="{ name: 'register' }" class="btn text-white fade-on-hover"><i class="fas fa-sign-in-alt"></i> Register</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated">
					<router-link :to="{ name: 'transporterHome' }" class="btn text-white fade-on-hover">Dashboard</router-link>
				</li>
				<li class="nav-item" v-if="isAuthenticated">
					<span class="btn text-white fade-on-hover pointer" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
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
		}
	}
}
</script>