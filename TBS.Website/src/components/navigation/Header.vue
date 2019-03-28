<template>
	<nav class="navbar navbar-expand-lg navbar-light bg-blue">
			<router-link :to="{ name: 'home' }" class="navbar-brand fade-on-hover text-white">Transportation Bidding System</router-link>
			<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
				<span class="navbar-toggler-icon"></span>
			</button>
			<div class="collapse navbar-collapse text-center" id="navigationBar">
				<ul class="navbar-nav mr-auto">
					<li class="nav-item">
						<router-link :to="{ name: 'home' }" class="btn text-white fade-on-hover">Home</router-link>
					</li>
					<!-- TODO: User must be logged in -->
					<li class="nav-item">
						<router-link :to="{ name: 'createPost' }" class="btn text-white fade-on-hover">Create a Post</router-link>
					</li>
					<!-- TODO: If user is a dealer show TSP posts instead of dealerPosts -->
					<li class="nav-item">
						<router-link :to="{ name: 'dealerPosts' }" class="btn text-white fade-on-hover">View Posts</router-link>
					</li>
				</ul>
				<ul class="navbar-nav ml-auto">
					<!-- TODO: User must not be logged in -->
					<li class="nav-item">
						<router-link :to="{ name: 'login' }" class="btn text-white fade-on-hover"><i class="fas fa-sign-in-alt"></i> Login</router-link>
					</li>
					<!-- TODO: User must not be logged in -->
					<li class="nav-item">
						<router-link :to="{ name: 'register' }" class="btn text-white fade-on-hover"><i class="fas fa-sign-in-alt"></i> Register</router-link>
					</li>
					<!-- TODO: User must be logged in -->
					<li class="nav-item">
						<router-link :to="{ name: 'managePosts' }" class="btn text-white fade-on-hover">Manage Posts</router-link>
					</li>
					<!-- TODO: User must be logged in -->
					<li class="nav-item">
							<span class="btn text-white fade-on-hover" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
					</li>
				</ul>
		</div>
	</nav>
</template>

<script>
export default {
	name: 'header',

}
</script>