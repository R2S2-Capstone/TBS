<template>
  <div class="row">
    <div v-for="(post, index) in posts" :key="index" class="col-12">
      <Post :post="post" />
    </div>
  </div>
</template>

<script>
import Post from '@/components/Card/Post.vue'

export default {
  name: 'posts',
  components: {
    Post,
  },
  props: {
    posts: Array
  }
}
</script>