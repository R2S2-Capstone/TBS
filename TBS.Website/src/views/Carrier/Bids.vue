<template>
  <div class="container pt-5">
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
            <tbody>
              <tr v-for="bid in bids" :key="bid.id">
                <td>{{ bid.shipper.name }}</td>
                <td>{{ format(bid.bidAmount) }}</td>
                <td>COMING SOON <i class="fas fa-star"></i></td>
                <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                <td v-if="bid.id"><router-link :to="{ name: 'carrierViewBidDetails', params: { id: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">View Details</router-link></td>
                <td >
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
            <li v-for="(page, index) in bidPageCount" :key="index" class="page-item" :class="page == currentBidPage ? 'active' : ''">
                <span class="page-link" @click="setBidPage(page)">{{ page }}</span>
            </li>
            <li class="page-item" :class="currentBidPage == bidPageCount || currentBidPage == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setBidPage(bidPageCount)">Last</span>
            </li>
            <li class="page-item" :class="currentBidPage == bidPageCount || currentBidPage == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setBidPage(currentBidPage+1)">Next</span>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Back from '@/components/Back.vue'

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
    acceptBid(bidId) {
      // this.bids.find(b => b.id == bidId).status = 'Accepted'
      //TODO: accept bid
    },
    declineBid(bidId) {
      // this.bids.find(b => b.id == bidId).status = 'Declined'
      //TODO: decline bid
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
        })
        .catch(() => {
          this.error = true
        })
    },
    fetchBids() {
      this.$store.dispatch('bids/getBidsByPostId', { type: 'carrier', postId: this.$route.params.id, currentPage: this.bidPage, pageSize: 5 })
        .then((response) => {
          this.bids = response.data.result.bids
          this.bidPageCount = response.data.result.paginationModel.totalPages
        })
        .catch(() => {
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
    this.fetchBids()
  }
}
</script>