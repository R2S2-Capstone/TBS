<template>
	<div>
		<Header/>
		<router-view/>
	</div>
</template>

<script>
import Header from '@/components/navigation/Header.vue'
export default {
	name: 'app',
	components: {
		Header
	}
}
</script>


<style lang="scss">
</style>
