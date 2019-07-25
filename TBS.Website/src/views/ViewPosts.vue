<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col-12">
        <h1 class="text-center">Coming Soon</h1>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'viewPosts',
  created() {
    this.$store.dispatch('posts/getPosts', { currentPage: 1, count: 0 })
    .then((response) => {
      console.log(JSON.stringify(response))
      // this.postPageCount = response.data.result.paginationModel.totalPages
      // this.posts = response.data.result.posts
    })
    .catch(() => {
      this.postsError = true
    })
  }
}
</script>
