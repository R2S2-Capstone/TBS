<template>
  <div class="container pt-5">
    <form @submit.prevent="searchPosts">
        <div class="row border p-3">
            <div class="col-12 text-center">
                <p class="text-danger" v-if="error">Failed to search for posts</p>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="pickupDate">After Pickup Date: </label>
                            <input type="Date" id="pickupDate" v-model="SearchModel.pickupDate" placeholder="Enter Pickup Date" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dropoffDate">Before Drop off Date: </label>
                            <input type="Date" id="keywords" v-model="SearchModel.dropoffDate" placeholder="Enter Dropoff date" class="form-control">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="pickupLocation">Enter Pickup City: </label>
                            <input type="text" id="city" v-model="SearchModel.pickupCity" placeholder="Type City here" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="dropoffLocation">Enter Dropoff City: </label>
                            <input type="text" id="city" v-model="SearchModel.dropoffCity" placeholder="Type City here" class="form-control">
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
    Posts,
  },
  data() {
    return {
      posts: [],
      error: false,
      currentPage: 1,
      pageCount: 1,
      pageSize: 9,
      SearchModel: {
        pickupDate: null,
        dropoffDate: null,
        pickupCity: "",
        dropoffCity: "",
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

     searchPosts() {
      var x = 0
      var y = 0
      if(this.SearchModel.pickupDate == null){
        this.SearchModel.pickupDate = ('1000-10-10')
        x = 1
      }
      if(this.SearchModel.dropoffDate == null){
        this.SearchModel.dropoffDate = ('1000-10-10')
        y = 1
      }
      this.$store.dispatch('posts/searchPosts', { currentPage: this.currentPage, pageSize: 9, SearchModel: this.SearchModel })
        .then((response) => {
          if(x == 1){
            x = 0;
            this.SearchModel.pickupDate = null
          }
          if(y == 1){
            y = 0;
            this.SearchModel.dropoffDate = null
          }
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
      this.searchPosts()
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
