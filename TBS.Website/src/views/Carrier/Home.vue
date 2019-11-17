<template>
  <div class="container pt-5 pb-5">
    <div class="row">
      <div class="col-12">
        <div class="row pb-3">
          <div class="col-12 text-center">
            <h2>Manage My Posts</h2>
            <h5 class="text-danger" v-if="postError">Unable to load posts</h5>
          </div>
        </div>
        <div class="row">
          <table class="table table-bordered table-hover text-center">
            <thead>
              <th style="width: 25%">Details</th>
              <th style="width: 25%">Status</th>
              <th style="width: 25%">Bids</th>
              <th style="width: 25%">Management</th>
            </thead>
            <tbody v-if="posts">
              <tr v-for="post in posts" :key="post.id">
                <td>
                  {{ `${post.pickupLocation} -> ${post.dropoffLocation}` }} <br>
                  {{ `Total Spaces Available: ${post.spacesAvailable}` }}
                </td>
                <td>{{ parsePostStatus(post.postStatus) }}</td>
                <td>
                  <router-link v-if="parsePostStatus(post.postStatus) == 'Open' || parsePostStatus(post.postStatus) == 'Pending Delivery' || parsePostStatus(post.postStatus) == 'Pending Delivery Approval'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'carrierManageBids', params: { id: post.id } }">Manage Bids</router-link>
                  <router-link v-else class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'carrierManageBids', params: { id: post.id } }">Archived Bids</router-link>
                </td>
                <td>
                  <router-link v-if="parsePostStatus(post.postStatus) == 'Open'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'carrierEditPost', params: { id: post.id } }">Edit</router-link>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="row">
          <div class="col-12">
            <ul class="pagination">
              <li class="page-item" :class="currentPostPage == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPostPage(currentPostPage-1)">Previous</span>
              </li>
              <li class="page-item" :class="currentPostPage == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPostPage(1)">First</span>
              </li>
              <li v-for="(page, index) in  postPageCount" :key="index" class="page-item" :class="page == currentPostPage ? 'active' : ''">
                <span class="page-link" @click="setPostPage(page)">{{ page }}</span>
              </li>
              <li class="page-item" :class="currentPostPage == postPageCount || postPageCount <= 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPostPage(postPageCount)">Last</span>
              </li>
              <li class="page-item" :class="currentPostPage == postPageCount || postPageCount <= 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPostPage(currentPostPage+1)">Next</span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="row pb-3">
          <div class="col-12 text-center">
            <h2>Manage My Bids</h2>
            <h5 class="text-danger" v-if="bidError">Unable to load bids</h5>
          </div>
        </div>
        <div class="row">
          <table class="table table-bordered table-hover text-center">
            <thead>
              <th>Address</th>
              <th>Amount</th>
              <th>Bid Status</th>
              <th>Management</th>
            </thead>
            <tbody>
              <tr v-for="bid in bids" :key="bid.id">
                <td>{{ bid.post.pickupLocation.addressLine }}</td>
                <td>{{ format(bid.bidAmount) }}</td>
                <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                <td>
                  <button v-if="parseBidStatus(bid.bidStatus) == 'Open'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mr-1" @click="confirmCancelBid(bid.id, bid.bidAmount)">Cancel</button>
                  <router-link v-if="parseBidStatus(bid.bidStatus) == 'Pending Delivery' || parseBidStatus(bid.bidStatus) == 'Pending Delivery Approval' || parseBidStatus(bid.bidStatus) == 'Completed'" :to="{ name: 'shipperDelivery', params: { postId: bid.post.id, bidId: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Delivery Information</router-link>     
                </td>
              </tr>
            </tbody>
          </table>
          <ul class="pagination">
            <li class="page-item" :class="currentBidPage == 1 ? 'disabled' : ''">
              <span class="page-link" @click="setBidPage(currentBidPage-1)">Previous</span>
            </li>
            <li class="page-item" :class="currentBidPage == 1 ? 'disabled' : ''">
              <span class="page-link" @click="setBidPage(1)">First</span>
            </li>
            <li v-for="(page, index) in  bidPageCount" :key="index" class="page-item" :class="page == currentBidPage ? 'active' : ''">
              <span class="page-link" @click="setBidPage(page)">{{ page }}</span>
            </li>
            <li class="page-item" :class="currentBidPage == bidPageCount || bidPageCount <= 1 ? 'disabled' : ''">
              <span class="page-link" @click="setBidPage(bidPageCount)">Last</span>
            </li>
            <li class="page-item" :class="currentBidPage == bidPageCount || bidPageCount <= 1 ? 'disabled' : ''">
              <span class="page-link" @click="setBidPage(currentBidPage+1)">Next</span>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import postUtilities from '@/utils/postUtilities.js'
import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'shipperHome',
  components: {
  },
  data() {
    return {
      posts: [],
      postPage: 1,
      postPageCount: 1,
      postError: false,
      bids: [],
      bidPage: 1,
      bidPageCount: 1,
      bidError: false,
    }
  },
  methods: {        
    format(number) {
      return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    },
    setPostPage(number) {
      if (number <= 0 || number > this.postPageCount) return
      this.postPage = number
      this.fetchPosts()
    },
    setBidPage(number) {
      if (number <= 0 || number > this.bidPageCount) return
      this.bidPage = number
      this.fetchBids()
    },
    confirmCancelBid(bidId, bidAmount) {
      Swal.fire({
        title: 'Are you sure?',
        text: `Are you sure you want to cancel this bid for $${bidAmount}?`,
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, cancel this bid!'
      }).then((result) => {
        if (result.value) {
          this.cancelBid(bidId)
        }
      })
    },
    cancelBid(bidId) {
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: bidId, bidStatus: 'cancelled' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 0
          Swal.fire({
            type: 'success',
            title: 'Cancelled',
            text: 'Bid has successfully been cancelled!',
          })
        })
    },
    fetchPosts() {
      this.$store.dispatch('posts/getMyPosts', { type: 'carrier', currentPage: this.postPage, pageSize: 5 })
        .then((response) => {
          this.postPageCount = response.data.result.paginationModel.totalPages
          this.posts = response.data.result.posts
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load your posts. Please try again!',
          })
          this.postError = true
        })
    },
    fetchBids() {
      this.$store.dispatch('bids/getMyBids', { type: 'shipper', currentPage: this.bidPage, pageSize: 5 })
        .then((response) => {
          this.bidPageCount = response.data.result.paginationModel.totalPages
          this.bids = response.data.result.bids
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load your bids. Please try again!',
          })
          this.bidError = true
        })
    },
    parsePostStatus(status) {
      return postUtilities.parsePostStatus(status)
    },
    parseBidStatus(status) {
      return bidUtilities.parseBidStatus(status)
    }
  },
  computed: {
    currentPostPage() {
      return this.postPage
    },
    currentBidPage() {
      return this.bidPage
    }
  },
  created() {
    this.fetchPosts()
    this.fetchBids()
  }
}
</script>