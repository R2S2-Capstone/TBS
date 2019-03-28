<template>
	<nav class="navbar navbar-expand-lg navbar-light bg-blue">
			<router-link :to="{ name: 'home' }" class="navbar-brand">Transportation Bidding System</router-link>
			<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
				<span class="navbar-toggler-icon"></span>
			</button>
			<div class="collapse navbar-collapse text-center" id="navigationBar">
				<ul class="navbar-nav mr-auto">
					<li class="nav-item">
						<router-link :to="{ name: 'home' }" class="btn">Home</router-link>
					</li>
				</ul>
				<ul class="navbar-nav ml-auto">
					<li class="nav-item">
						<router-link :to="{ name: 'login' }" class="btn"><i class="fas fa-sign-in-alt"></i> Login</router-link>
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