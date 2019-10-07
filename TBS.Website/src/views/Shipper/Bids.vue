<template>
  <div class="container pt-5 pb-5">
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
              <th style="width: 20%">Bidder</th>
              <th style="width: 20%">Amount</th>
              <th style="width: 20%">Rating</th>
              <th style="width: 20%">Status</th>
              <th style="width: 20%">Management</th>
            </thead>
            <tbody v-if="bids">
              <tr v-for="bid in bids" :key="bid.id">
                <td><router-link :to="{ name: 'carrierProfile', params: { id: bid.carrier.id }}" class="fade-on-hover text-blue">{{ bid.carrier.name }}</router-link></td>
                <td>{{ format(bid.bidAmount) }}</td>
                <td>COMING SOON <i class="fas fa-star"></i></td>
                <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                <td>
                  <div v-if="parseBidStatus(bid.bidStatus) == 'Open'">
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white m-3" @click="confirmAcceptBid(bid.id, bid.bidAmount, bid.carrier.name)">Accept</button>
                    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="confirmDeclineBid(bid.id, bid.bidAmount, bid.carrier.name)">Decline</button>
                  </div>
                  <router-link v-if="parseBidStatus(bid.bidStatus) == 'Pending Delivery' || parseBidStatus(bid.bidStatus) == 'Pending Delivery Approval' || parseBidStatus(bid.bidStatus) == 'Completed'" :to="{ name: 'shipperDelivery', params: { postId: post.id, bidId: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Delivery</router-link>                       
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
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: bidId, bidStatus: 'pendingDelivery' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 4
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
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: bidId, bidStatus: 'declined' })
        .then(() => {
          this.bids.find(b => b.id == bidId).bidStatus = 1
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