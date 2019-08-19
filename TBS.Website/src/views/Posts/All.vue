<template>
  <div class="container pt-5">
    <h5>Search coming soon...</h5>
    <Posts :posts="posts" />
    <ul class="pagination">
      <li class="page-item" :class="currentPage == 1 ? 'disabled' : ''">
        <span class="page-link" @click="setPage(currentPage-1)">Previous</span>
      </li>
      <li class="page-item" :class="currentPage == 1 ? 'disabled' : ''">
        <span class="page-link" @click="setPage(1)">First</span>
      </li>
      <li v-for="(page, index) in  pageCount" :key="index" class="page-item" :class="page == currentPage ? 'active' : ''">
        <span class="page-link" @click="setPage(page)">{{ page }}</span>
      </li>
      <li class="page-item" :class="currentPage == pageCount || pageCount <= 1 ? 'disabled' : ''">
        <span class="page-link" @click="setPage(pageCount)">Last</span>
      </li>
      <li class="page-item" :class="currentPage == pageCount || pageCount <= 1 ? 'disabled' : ''">
        <span class="page-link" @click="setPage(currentPage + 1)">Next</span>
      </li>
    </ul>
  </div>
</template>

<script>
import Posts from '@/components/Posts.vue'

export default {
  name: 'viewPosts',
  components: {
    Posts
  },
  data() {
    return {
      posts: [],
      error: false,
      currentPage: 1,
      pageCount: 1,
      pageSize: 9,
    }
  },
  methods: {
    getAllPosts() {
      this.$store.dispatch('posts/getPosts', { currentPage: this.currentPage, pageSize: 9 })
        .then((response) => {
          this.posts = response.data.result.posts
          this.postPageCount = response.data.result.paginationModel.totalPages
        })
        .catch(() => {
          this.postsError = true
        })
    },
    setPage(pageIndex) {
      if (pageIndex <= 0 || pageIndex > this.pageCount) return
      this.currentPage = pageIndex
      this.getAllPosts()
    },
  },
  created() {
    this.getAllPosts()
  },
  computed: {
    c() {
      return this.currentPage
    },
  }
}
</script>
