<template>
  <Home v-if="!viewPosts" />
  <ViewAllPosts v-else />
</template>

<script>
import Home from '@/views/Home.vue'
import ViewAllPosts from '@/views/Posts/All.vue'

export default {
  name: 'home',
  props: {
    viewPosts: Boolean
  },
  components: {
    Home,
    ViewAllPosts
  },
}
</script>