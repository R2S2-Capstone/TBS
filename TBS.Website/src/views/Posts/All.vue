<template>
  <div class="container pt-5">
    <form @submit.prevent="search">
        <div class="row border p-3">
            <div class="col-12 text-center">
                <p class="text-danger" v-if="error">Failed to search for posts</p>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="pickupDate">Pickup Date: </label>
                            <input type="Date" id="pickupDate" v-model="searchModel.pickupDate" placeholder="Enter Pickup Date" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dropoffDate">Drop off Date: </label>
                            <input type="text" id="keywords" v-model="searchModel.dropoffLocation" placeholder="Enter Dropoff date" class="form-control">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="pickupLocation">Enter Pickup City Location: </label>
                            <input type="text" id="city" v-model="searchModel.pickupLocation" placeholder="Type City here" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dropoffLocation">Enter Dropoff City Location: </label>
                            <input type="text" id="city" v-model="searchModel.dropoffLocation" placeholder="Type City here" class="form-control">
                        </div>
                    </div>
                </div>
                <div class="form-group text-center">
                    <button type="submit" class="btn btn-main bg-blue fade-on-hover text-uppercase">Search</button>
                </div>
            </div>
        </div>
    </form>
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
import Swal from 'sweetalert2'

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
      searchModel: {
        pickupDate: "10/10/1000",
        dropoffDate: "10/10/1000",
        pickupLocation: null,
        dropoffLocation: null,
       }
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
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load these posts. Please try again!',
          })
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
