<template>
	<div>
		<TheHeader/>
		<router-view/>
	</div>
</template>

<script>
import TheHeader from '@/components/navigation/Header.vue'
export default {
	name: 'app',
	components: {
		TheHeader
	}
}
</script>


<style lang="scss">
@import "@/assets/scss/global.scss";
* {
    padding: 0;
    margin: 0;
    font-family: 'Montserrat', sans-serif;
    color: color(mainText);
}
.bg-blue {
	background-color: color(colorPrimary);
}
</style>
