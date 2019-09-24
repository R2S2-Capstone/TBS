<template>
  <div class="container pt-5 pb-5">
    <Back/>
    <WideCard v-if="bid" :title="'Bid from ' + bid.shipper.name">
      <div slot="card-content" class="text-center">
        <div class="row">
          <div class="col-12">
            <div class="row">
              <div class="col-12">
                <h4>Bid Status: {{ parseBidStatus(bid.bidStatus) }}</h4>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <hr>
                <h5>Vehicle Information</h5>
                <hr>
                <p>Vehicle Make: {{ bid.vehicle.make }}</p>
                <p>Vehicle Model: {{ bid.vehicle.model }}</p>
                <p>Vehicle Year: {{ bid.vehicle.year }}</p>
                <p>Vehicle Condition: {{ parseVehicleCondition(bid.vehicle.condition) }}</p>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <hr>
                <h5>Bid Details</h5>
                <hr>
                <p>Amount: ${{ bid.bidAmount }}</p>
                <p>Bidder Name: {{ bid.shipper.name }}</p>
                <p>Bidder Rating: COMING SOON <i class="fas fa-star"></i></p>
              </div>
            </div>
          </div>

          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <hr>
              <h5>Pickup Information</h5>
              <hr>
              <p>Pickup Location: {{ formatAddress(bid.pickupLocation) }}</p>
              <p>Pickup Date: {{ bid.pickupDate.split('T')[0] }}</p>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <hr>
              <h5>Dropoff Information</h5>
              <hr>
              <p></p>
              <p>Dropoff Location: {{ formatAddress(bid.dropoffLocation) }}</p>
              <p>Dropoff Date: {{ bid.dropoffDate.split('T')[0] }}</p>
            </div>
          </div>

          <div class="col-12" v-if="parseBidStatus(bid.bidStatus) == 'Open'">
            <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mb-3" @click="confirmAcceptBid(bid.id, bid.bidAmount, bid.shipper.name)">Accept Bid</button><br>
            <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="confirmDeclineBid(bid.id, bid.bidAmount, bid.shipper.name)">Decline Bid</button>
          </div>
        </div>
      </div>
    </WideCard>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import Back from '@/components/Back.vue'
import WideCard from '@/components/Card/WideCard.vue'

import postUtilities from '@/utils/postUtilities.js'
import bidUtilities from '@/utils/bidUtilities.js'

export default {
  name: 'carrierViewBidDetails',
  components: {
    Back,
    WideCard,
  },
  beforeCreate() {
    // A bid id must be passed, if not return to previous route
    if (this.$route.params.id == null) this.$router.go(-1)
  },
  data() {
    return {
      bid: null,
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
          this.bid.bidStatus = 4
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
          this.bid.bidStatus = 1
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
    fetchBid() {
      this.$store.dispatch('bids/getBidById', { type: 'carrier', bidId: this.$route.params.id })
        .then((response) => {
          this.bid = response.data.result
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to load this bid. Please try again!',
          })
        })
    },
    formatAddress(address) {
      return postUtilities.formatAddress(address)
    },
    parseVehicleCondition(condition) {
      return postUtilities.parseVehicleCondition(condition)
    },
    parseBidStatus(status) {
      return bidUtilities.parseBidStatus(status)
    }
  },
  created() {
    this.fetchBid()
  }
}
</script>
