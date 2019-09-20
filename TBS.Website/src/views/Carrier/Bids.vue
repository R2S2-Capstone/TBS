<template>
  <div class="container pt-5 pb-5">
    <Back/>
    <div class="row">
      <div class="col-12">
        <div class="row pb-3" v-if="post">
          <div class="col-12 text-center">
            <h2>Manage Bids for {{ post.pickupLocation }} ({{ post.pickupDate.split('T')[0] }}) - {{ post.dropoffLocation }} ({{ post.dropoffDate.split('T')[0] }})</h2>
            <p class="text-danger" v-if="error">Failed to load post details</p>
          </div>
        </div>
        <div class="row">
          <table class="table table-bordered table-hover text-center">
            <thead>
              <th>Bidder</th>
              <th>Amount</th>
              <th>Rating</th>
              <th>Status</th>
              <th>Details</th>
              <th>Management</th>
            </thead>
            <tbody v-if="bids">
              <tr v-for="bid in bids" :key="bid.id">
                <td>{{ bid.shipper.name }}</td>
                <td>{{ format(bid.bidAmount) }}</td>
                <td>COMING SOON <i class="fas fa-star"></i></td>
                <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                <td v-if="bid.id"><router-link :to="{ name: 'carrierViewBidDetails', params: { id: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">View Details</router-link></td>
                <td >
                  <div v-if="parsePostStatus(post.postStatus) == 'Open' && parseBidStatus(bid.bidStatus) == 'Open'">
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mr-1" @click="confirmAcceptBid(bid.id, bid.bidAmount, bid.shipper.name)">Accept</button>
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="confirmAcceptBid(bid.id, bid.bidAmount, bid.shipper.name)">Decline</button>
                  </div>
                  <router-link v-if="parseBidStatus(bid.bidStatus) == 'Pending Delivery' || parseBidStatus(bid.bidStatus) == 'Pending Delivery Approval' || parseBidStatus(bid.bidStatus) == 'Completed'" :to="{ name: 'carrierDelivery', params: { postId: post.id, bidId: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">View</router-link>                       
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
            <li v-for="(page, index) in bidPageCount" :key="index" class="page-item" :class="page == currentBidPage ? 'active' : ''">
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

import Back from '@/components/Back.vue'

import postUtilities from '@/utils/postUtilities.js'
import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'carrierManageBids',
  components: {
    Back
  },
  beforeCreate() {
    // A post ID must be passed, if not return to previous route
    if (this.$route.params.id == null) this.$router.go(-1)
  },
  data() {
    return {
      bidPage: 1,
      bidPageCount: 1,
      error: false,
      post: null,
      bids: [],
    }
  },
  methods: {
    confirmAcceptBid(bidId, bidAmount, bidFrom) {
        Swal.fire({
        title: 'Are you sure?',
        text: `Are you sure you want to accept this bid for $${bidAmount} from ${bidFrom}?`,
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, accept this bid!'
      }).then((result) => {
        if (result.value) {
          this.acceptBid(bidId)
        }
      })
    },
    acceptBid(bidId) {
      this.$store.dispatch('bids/updateBid', { type: 'carrier', bidId: bidId, bidStatus: 'pendingDelivery' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 3
          Swal.fire({
            type: 'success',
            title: 'Accepted',
            text: 'Bid has successfully been accepted!',
          })
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! Please try again!',
          })
        })
    },
    confirmDeclineBid(bidId, bidAmount, bidFrom) {
      Swal.fire({
        title: 'Are you sure?',
        text: `Are you sure you want to decline this bid for $${bidAmount} from ${bidFrom}?`,
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, decline this bid!'
      }).then((result) => {
        if (result.value) {
          this.declineBid(bidId)
        }
      })
    },
    declineBid(bidId) {
      this.$store.dispatch('bids/updateBid', { type: 'carrier', bidId: bidId, bidStatus: 'declined' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 2
          Swal.fire({
            type: 'success',
            title: 'Declined',
            text: 'Bid has successfully been declined!',
          })
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! Please try again!',
          })
        })
    },
    format(number) {
      return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    },
    setBidPage(number) {
      if (number <= 0 || number > this.bidPageCount) return
      this.bidPage = number
      this.fetchBids()
    },
    fetchPost() {
      this.$store.dispatch('posts/getPostById', { type: 'carrier', postId: this.$route.params.id })
        .then((response) => {
          this.post = response.data.result
          this.bids = this.post.bids
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load this post. Please try again!',
          })
          this.error = true
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
    currentBidPage() {
      return this.bidPage
    }
  },
  created() {
    this.fetchPost()
  }
}
</script>