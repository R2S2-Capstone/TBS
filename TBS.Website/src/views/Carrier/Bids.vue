<template>
  <div class="container pt-5">
    <Back/>
    <div class="row">
      <div class="col-12">
        <div class="row pb-3">
          <div class="col-12 text-center">
            <h2>Manage Bids for {{ post.pickupCity }} ({{ post.pickupDate }}) - {{ post.deliveryCity }} ({{ post.deliveryDate }})</h2>
            <p class="text-success" v-if="post.acceptedBid">You have already accepted a bid</p>
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
                <td>{{ bid.bidder.name }}</td>
                <td>{{ format(bid.amount) }}</td>
                <td>{{ bid.bidder.rating }} <i class="fas fa-star"></i></td>
                <td>{{ bid.status }}</td>
                <td><router-link :to="{ name: 'carrierViewBidDetails', params: { id: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">View Details</router-link></td>
                <td >
                  <div v-if="bid.status == 'Pending'">
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
      post: {},
      bids: [],
    }
  },
  methods: {
    acceptBid(bidId) {
      this.bids.find(b => b.id == bidId).status = 'Accepted'
    },
    declineBid(bidId) {
      this.bids.find(b => b.id == bidId).status = 'Declined'
    },
    format(number) {
      return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    },
    setPostPage(number) {
      if (number <= 0 || number > this.postPageCount) return
      this.postPage = number
      // TODO: filter based on these results
    },
    setBidPage(number) {
      if (number <= 0 || number > this.bidPageCount) return
      this.bidPage = number
      // TODO: filter based on these results
    },
  },
  computed: {
    currentPostPage() {
      return this.postPage
    },
    currentBidPage() {
      return this.bidPage
    }
  }
}
</script>