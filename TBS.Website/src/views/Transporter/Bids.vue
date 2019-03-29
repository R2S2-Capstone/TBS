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
                <td><router-link :to="{ name: 'transporterViewBidDetails', params: { id: bid.id } }" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">View Details</router-link></td>
                <td >
                  <div v-if="bid.status == 'Pending' && !post.acceptedBid">
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
  name: 'manageTransporterBids',
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
      post: {
        pickupCity: 'Oakville',
        pickupDate: '03/28/2019',
        deliveryCity: 'Brampton',
        deliveryDate: '03/29/2019',
        acceptedBid: false,
      },
      bids: [
        {
          id: '1',
          bidder: {
            name: 'Reece Rose',
            rating: 5,
          },
          status: 'Pending',
          amount: 5000,
        },
        {
          id: '2',
          bidder: {
            name: 'Sathira Paduka',
            rating: 1.2,
          },
          status: 'Pending',
          amount: 5100,
        },
        {
          id: '3',
          bidder: {
            name: 'Robert Middlebrook',
            rating: 2,
          },
          status: 'Pending',
          amount: 5050,
        },
        {
          id: '4',
          bidder: {
            name: 'Steven Boxall',
            rating: 1,
          },
          status: 'Declined',
          amount: 5000,
        }
      ]
    }
  },
  methods: {
    acceptBid(bidId) {
      this.post.acceptedBid = true
    },
    declineBid(bidId) {
      console.log(`Decline bid ${bidId}`)
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