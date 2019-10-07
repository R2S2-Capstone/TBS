<template>
  <div class="container pt-5 pb-5" v-if="post">
    <Back />
    <div class="row text-center">
      <div class="col-12">
        <h2>{{ formatAddress(post.pickupLocation) }} -><br> {{ formatAddress(post.dropoffLocation) }}</h2>
        <h4 v-if="error" class="text-danger mb-5">Failed to load post information</h4>
      </div>
    </div>
    <div class="row pt-2 text-center">
      <div class="col-12">
        <ul class="progress-bar-list d-flex justify-content-center">
          <li :class="(convertedBidStatus == 'Pending Delivery' || convertedBidStatus == 'Pending Delivery Approval' || convertedBidStatus == 'Completed') == true ? 'active' :''">Pending Delivery</li>
          <li :class="(convertedBidStatus == 'Pending Delivery Approval' || convertedBidStatus == 'Completed') == true ? 'active' : ''">Pending Delivery Approval</li>
          <li :class="(convertedBidStatus == 'Completed') == true ? 'active' : ''">Completed</li>
        </ul>
      </div>
    </div>
    <div class="row pt-3 text-center">
      <div class="col-12 background pt-3">
        <h4>Post Details</h4>
        <hr>
        <div class="row">
          <div class="col-12">
            <p></p>
            <div>
              <table class="table">
                <thead>
                  <th style="width: 33.3%">Departure</th>
                  <th style="width: 33.3%">Destination</th>
                  <th style="width: 33.3%">Vehicle Information</th>
                </thead>
                <tbody>
                  <td>{{ formatAddress(post.pickupLocation) }} - {{ parseDate(post.pickupDate) }}</td>
                  <td>{{ formatAddress(post.dropoffLocation) }} - {{ parseDate(post.dropoffDate) }}</td>
                  <td>{{ `${post.vehicle.year} ${post.vehicle.make} ${post.vehicle.model}` }}</td>
                </tbody>
              </table>
              <table class="table">
                <thead>
                  <th style="width: 50%">Pickup Contact</th>
                  <th style="width: 50%">Dropoff Contact</th>
                </thead>
                <tbody>
                  <td>
                    Name: {{ post.pickupContact.name }}<br>
                    Phone Number: <a :href="'tel:' + post.pickupContact.phoneNumber">{{ post.pickupContact.phoneNumber }}</a><br>
                    Email: <a :href="'mailto:' + post.pickupContact.email">{{ post.pickupContact.email }}</a><br>
                  </td>
                  <td>
                    Name: {{ post.dropoffContact.name }}<br>
                    Phone Number: <a :href="'tel:' + post.dropoffContact.phoneNumber">{{ post.dropoffContact.phoneNumber }}</a><br>
                    Email: <a :href="'mailto:' + post.dropoffContact.email">{{ post.dropoffContact.email }}</a><br>
                  </td>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row pt-3 text-center">
      <div class="col-12 background pt-3">
        <h4>Bid Details</h4>
        <hr>
        <div class="row">
          <div class="col-12">
            <p></p>
            <div>
              <table class="table">
                <tr>
                  <th style="width: 33.3%">Carrier</th>
                  <th style="width: 33.3%">Bid Amount</th>
                  <th style="width: 33.3%">Status</th>
                </tr>
                <tr>
                  <td><router-link :to="{ name: 'carrierProfile', params: { id:bid.carrier.id }}" class="fade-on-hover text-blue">{{ bid.carrier.name }}</router-link></td>
                  <td>{{ formatMoney(bid.bidAmount) }}</td>
                  <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row pt-3 text-center" v-if="!$store.getters['global/isLoading']">
      <div class="col-12 pt-3 pb-3">
        <div class="fixed-bottom pb-btn text-center">
          <button v-if="accountType == 'carrier' && convertedBidStatus == 'Pending Delivery'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="updateBid('Pending Delivery Approval')">Confirm Delivery</button>
          <button v-if="accountType == 'shipper' && convertedBidStatus == 'Pending Delivery'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="updateBid('Completed')">Force Delivery</button>
          <button v-if="accountType == 'shipper' && convertedBidStatus == 'Pending Delivery Approval'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="updateBid('Completed')">Approve Delivery</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Back from '@/components/Back.vue'

import Swal from 'sweetalert2'

import postUtilities from '@/utils/postUtilities.js'
import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'shipperDelivery',
  components: {
    Back
  },
  props: {
    postId: String,
    bidId: String,
  },
  data() {
    return {
      accountType: '',
      postType: '',
      post: null,
      bid: null,
      error: null,
      convertedBidStatus: null,
    }
  },
  methods: {
    loadDetails() {
      this.$store.dispatch('posts/getPostById', { type: 'shipper', postId: this.$route.params.postId })
        .then((response) => {
          this.post = response.data.result
          this.bid = this.post.bids.find((bid) => bid.id == this.bidId)
          this.convertedBidStatus = this.parseBidStatus(this.bid.bidStatus)
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load the delivery details. Please try again!',
          })
          this.error = true
        })
    },
    updateBid(newStatus) {
      this.$store.dispatch('bids/updateBid', { type: 'shipper', bidId: this.bid.id, bidStatus: this.reverseBidStatus(newStatus.toLowerCase()) })
        .then(() => {
          this.bid.bidStatus = this.reverseBidStatus(newStatus.toLowerCase())
          this.convertedBidStatus = this.parseBidStatus(this.bid.bidStatus)
          Swal.fire({
            type: 'success',
            title: 'Updated',
            text: 'Bid has successfully been updated!',
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
    parsePostStatus(status) {
      return postUtilities.parsePostStatus(status)
    },
    reverseBidStatus(status) {
      return bidUtilities.reverseBidStatus(status)
    },
    parseBidStatus(status) {
      return bidUtilities.parseBidStatus(status)
    },
    parseTrailerType(type) {
      return postUtilities.parseTrailerType(type)
    },
    parseDate(time) {
      return postUtilities.parseDate(time)
    },
    formatAddress(address) {
      return postUtilities.formatAddress(address)
    },
    formatMoney(money) {
      return postUtilities.formatMoney(money)
    }
  },
  created() {
    if (this.postId == null || this.bidId == null) this.$router.go(-1)
    this.accountType = this.$store.getters['authentication/getAccountType'].toLowerCase()
    this.loadDetails()
  }
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss";

.progress {
  height: auto;
  .progress-bar {
    h6 {
      font-weight: 500;
    }
  }
}
.background {
  background-color: #fff;
  border: 1px solid #dbdbdb;
  border-radius: 7.5px;
}
hr {
  margin: 0 auto;
  width: 15vw;
  max-width: 100%;
  border-top: 1px solid colour(colourPrimary);
}

.progress-bar-list {
  counter-reset: step;
  li {
    list-style-type: none;
    width: 25%;
    float: left;
    font-size: 12px;
    position: relative;
    text-align: center;
    text-transform: uppercase;
    color: #7d7d7d;

    &::before {
      width: 30px;
      height: 30px;
      content: counter(step);
      // padding-bottom: 5px;
      counter-increment: step;
      line-height: 30px;
      border: 2px solid #7d7d7d;
      display: block;
      text-align: center;
      margin: 0 auto 10px auto;
      border-radius: 50%;
      background-color: white;
    }

    &:after {
      width: 100%;
      height: 2px;
      content: '';
      position: absolute;
      background-color: #7d7d7d;
      top: 15px;
      left: -50%;
      z-index: -1;
    }

    &:first-child:after {
      content: none;
    }
  }

  .active {
    &:before {
      border-color: colour(colourPrimary);
    }
    &:after {
      background-color: colour(colourPrimary);
    }
  }
}

.pb-btn {
  @include  mobile {
    padding-bottom: 125px;
  }
  @include tablet {
    padding-bottom: 115px;
  }
  @include desktop {
    padding-bottom: 105px;
  }
}
</style>