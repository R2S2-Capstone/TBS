<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col-12">
        <div class="row pb-3">
          <div class="col-12 text-center">
            <h2>Manage My Posts</h2>
          </div>
        </div>
        <div class="row">
          <table class="table table-bordered table-hover text-center">
            <thead>
              <th>Address</th>
              <th>Status</th>
              <th>Bids</th>
              <th>Management</th>
            </thead>
            <tbody>
              <tr v-for="post in posts" :key="post.id">
                <td>{{ post.address }}</td>
                <td>{{ post.status }}</td>
                <td>
                  <router-link v-if="post.status == 'Pending Approval'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'transporterManageBids', params: { id: post.id } }">Manage Bids</router-link>
                  <router-link v-else class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'transporterManageBids', params: { id: post.id } }">View Bids</router-link>
                </td>
                <td>
                  <router-link v-if="post.status == 'Pending Approval'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" :to="{ name: 'transporterEditPost', params: { id: post.id } }">Edit</router-link>
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
              <li class="page-item" :class="currentPostPage == postPageCount || currentPostPage == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPostPage(postPageCount)">Last</span>
              </li>
              <li class="page-item" :class="currentPostPage == postPageCount || currentPostPage == 1 ? 'disabled' : ''">
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
                <td>{{ bid.address }}</td>
                <td>{{ format(bid.amount) }}</td>
                <td>{{ bid.bidStatus }}</td>
                <td><button v-if="bid.bidStatus == 'Pending'" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mr-1" @click="cancelBid(bid.id)">Cancel</button></td>
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
export default {
  name: 'dealerHome',
  components: {
  },
  data() {
    return {
      postPage: 1,
      postPageCount: 1,
      bidPage: 1,
      bidPageCount: 1,
      posts: [
        {
          id: '1',
          address: '1430 Trafalgar Rd, Oakville, ON L6H 2L1',
          status: 'In Transport'
        },
        {
          id: '12',
          address: '1430 Trafalgar Rd, Oakville, ON L6H 2L1',
          status: 'Pending Approval'
        },
        {
          id: '123',
          address: '1430 Trafalgar Rd, Oakville, ON L6H 2L1',
          status: 'Delivered'
        }
      ],
      bids: [
        {
          id: '1',
          address: '1430 Trafalgar Rd, Oakville, ON L6H 2L1',
          amount: 5000,
          bidStatus: 'Approved'
        },
        {
          id: '2',
          address: '1450 Trafalgar Rd, Oakville, ON L6H 2L1',
          amount: 1000,
          bidStatus: 'Pending'
        },
        {
          id: '3',
          address: '1450 Trafalgar Rd, Oakville, ON L6H 2L1',
          amount: 5000,
          bidStatus: 'Declined'
        }
      ]
    }
  },
  methods: {        
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
    cancelBid(bidId) {
      this.bids.find(b => b.id == bidId).bidStatus = 'Cancelled'
    }
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