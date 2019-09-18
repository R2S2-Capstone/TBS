<template>
  <div class="container pt-5">
    <Back/>
    <div class="row">
      <div class="col-12">
        <div class="row pb-3" v-if="post">
          <div class="col-12 text-center">
            <h2>
              Manage Bids for {{ post.pickupLocation.city }} <i class="fas fa-arrow-right"></i> {{ post.dropoffLocation.city }}
            </h2>
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
              <th>Management</th>
            </thead>
            <tbody v-if="bids">
              <tr v-for="bid in bids" :key="bid.id">
                <td>{{ bid.carrier.name }}</td>
                <td>{{ format(bid.bidAmount) }}</td>
                <td>COMING SOON <i class="fas fa-star"></i></td>
                <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                <td>
                  <div v-if="parseBidStatus(bid.bidStatus) == 'Open'">
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mr-1" @click="acceptBid(bid.id)">Accept</button>
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="declineBid(bid.id)">Decline</button>
                  </div>
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

import Back from '@/components/Back.vue'

import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'shipperManageBids',
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
    acceptBid(bidId) {
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: bidId, bidStatus: 'approved' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 1
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
    declineBid(bidId) {
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: bidId, bidStatus: 'declined' })
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
      this.$store.dispatch('posts/getPostById', { type: 'shipper', postId: this.$route.params.id })
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

<style lang="scss" scoped>
.fa-star {
  color: #efce4a;
}
</style>