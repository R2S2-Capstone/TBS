<template>
  <div class="container pt-5" v-if="post">
    <div class="row text-center">
      <div class="col-12">
        <h2 v-if="postType == 'carrier'">Delivery for {{ post.pickupLocation }} -> {{ post.dropoffLocation }}</h2>
        <h2 v-else>Delivery for {{ post.pickupLocation.city }} -> {{ post.dropoffLocation.city }}</h2>
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
                  <th>Departure</th>
                  <th>Destination</th>
                  <th>Vehicle Information</th>
                </thead>
                <tbody>
                  <td>{{ postType == 'carrier' ? post.pickupLocation : post.pickupLocation.city }} - {{ trimDate(post.pickupDate) }}</td>
                  <td>{{ postType == 'carrier' ? post.dropoffLocation : post.dropoffLocation.city }} - {{ trimDate(post.dropoffDate) }}</td>
                  <td>{{ postType == 'carrier' ? `${post.carrier.vehicle.year} ${post.carrier.vehicle.make} ${post.carrier.vehicle.model} (${parseTrailerType(post.carrier.vehicle.trailerType)})` : `${post.vehicle.year} ${post.vehicle.make} ${post.vehicle.model}` }}</td>
                </tbody>
              </table>
              <table class="table">
                <thead>
                  <th>Pickup Contact</th>
                  <th>Dropoff Contact</th>
                </thead>
                <tbody>
                  <td>
                    Name: {{ postType == 'carrier' ? post.carrier.name : post.pickupContact.name }}
                    <p v-if="postType != 'carrier'" class="m-0">Phone Number: <a :href="'tel:' + post.pickupContact.phoneNumber">{{ post.pickupContact.phoneNumber }}</a></p>
                    Email: <a :href="'mailto:' + postType == 'carrier' ? post.carrier.email : post.pickupContact.email">{{ postType == 'carrier' ? post.carrier.email : post.pickupContact.email }}</a>
                  </td>
                  <td>
                    Name: {{ postType == 'carrier' ? post.carrier.name : post.dropoffContact.name }}
                    <p v-if="postType != 'carrier'" class="m-0">Phone Number: <a :href="'tel:' + post.dropoffContact.phoneNumber">{{ post.dropoffContact.phoneNumber }}</a></p>
                    Email: <a :href="'mailto:' + postType == 'carrier' ? post.carrier.email : post.dropoffContact.email">{{ postType == 'carrier' ? post.carrier.email : post.dropoffContact.email }}</a>
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
                  <th>Carrier</th>
                  <th>Bid Amount</th>
                  <th>Status</th>
                </tr>
                <tr>
                  <!-- TODO: Generate router-link to profile page -->
                  <td><router-link :to="{ name: 'home' }" class="fade-on-hover text-blue">{{ postType == 'carrier' ? bid.shipper.name : bid.carrier.name }}</router-link></td>
                  <td>${{ bid.bidAmount }}</td>
                  <td>{{ parseBidStatus(bid.bidStatus) }}</td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row pt-3 pb-5 text-center">
      <div class="col-12 pt-3">
        <button v-if="accountType == 'carrier' && convertedBidStatus == 'Pending Delivery'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="confirmDelivery">Confirm Delivery</button>
        <button v-if="accountType == 'shipper' && convertedBidStatus == 'Pending Delivery'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="forceDelivery">Force Delivery</button>
        <button v-if="accountType == 'shipper' && convertedBidStatus == 'Pending Delivery Approval'" type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="confirmDelivery">Approve Delivery</button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import postUtilities from '@/utils/postUtilities.js'
import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'delivery',
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
      // this.$store.dispatch('posts/getPostById', { type: this.postType, postId: this.$route.params.postId })
      this.$store.dispatch('posts/getPostById', { type: this.postType, postId: 'ea2ceb02-fdea-4529-6afa-08d73bdaaba3' })
        .then((response) => {
          this.post = response.data.result
          // this.bid = this.post.bids.find((bid) => bid.id = this.bidId)
          this.bid = this.post.bids.find((bid) => bid.id = 'aeddc9ab-9ff2-48fb-88e7-08d73bdab51b')
          this.convertedBidStatus = this.parseBidStatus(this.bid.bidStatus)
          console.log(this.bid)
          console.log(this.post)
        })
        .catch((error) => {
          console.log(error)
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load the delivery details. Please try again!',
          })
          this.error = true
        })
    },
    confirmDelivery() {

    },
    forceDelivery() {

    },
    parsePostStatus(status) {
      return postUtilities.parsePostStatus(status)
    },
    parseBidStatus(status) {
      return bidUtilities.parseBidStatus(status)
    },
    parseTrailerType(type) {
      return postUtilities.parseTrailerType(type)
    },
    trimDate(time) {
      return postUtilities.trimDate(time)
    }
  },
  created() {
    if (this.postId == null || this.bidId == null) this.$router.go(-1)
    this.$store.watch(
      (state) => {
        return this.$store.getters['authentication/getAccountType']
      },
      (value) => {
        this.accountType = value.toLowerCase()
        this.postType = this.accountType == 'carrier' ? 'shipper' : 'carrier'
        this.loadDetails()
      }, { deep:true }
    )
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
</style>