<template>
	<div>
		<TheHeader/>
		<router-view/>
		<TheFooter/>
	</div>
</template>

<script>
import TheHeader from '@/components/navigation/Header.vue'
import TheFooter from '@/components/navigation/Footer.vue'
export default {
	name: 'app',
	components: {
		TheHeader,
		TheFooter
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
body {
	margin-bottom: 56px;
}
.fade-on-hover {
    &:hover {
        opacity: 0.75;
    }
}
.bg-blue {
	background-color: color(colorPrimary);
}
</style>
