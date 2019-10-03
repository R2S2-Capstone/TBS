<template>
  <div class="container pt-5 pb-5">
    <div class="row" v-if="error">
      <div class="col-12 text-center">
        <h4 class="text-danger mb-5">Failed to load post...</h4>
        <h6 @click="getPostById()" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to try again</h6><br>
        <h6 @click="$router.go(-1)" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to return</h6>
      </div>
    </div>
    <div v-if="!error && post != null">
      <div class="row pt-3 text-center">
        <div class="col-12 background pt-3">
          <h5>Post Details</h5>
          <hr>
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th></th>
                  <th>Address</th>
                  <th>Date</th>
                </tr>
                <tr>
                  <th>Pickup In: </th>
                  <td>{{ formatAddress(post.pickupLocation) }}</td>
                  <td>{{ parseDate(post.pickupDate) }}</td>
                </tr>
                <tr>
                  <th>Deliver To: </th>
                  <td>{{ formatAddress(post.dropoffLocation) }}</td>
                  <td>{{ parseDate(post.dropoffDate) }}</td>
                </tr>
              </table>
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Vehicle Information</h5>
              <hr>
              <div class="row pt-3">
                <div class="col-12">
                  <table class="table">
                    <tr>
                      <th style="width: 25%">Condition</th>
                      <th style="width: 25%">Year</th>
                      <th style="width: 25%">Make</th>
                      <th style="width: 25%">Model</th>
                    </tr>
                    <tr>
                      <td>{{ parseVehicleCondition(post.vehicle.condition) }}</td> 
                      <td>{{ post.vehicle.year }}</td> 
                      <td>{{ post.vehicle.make }}</td> 
                      <td>{{ post.vehicle.model }}</td> 
                    </tr>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12 background pt-3">
          <h5>Shipper Information</h5>
          <hr>
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 33.3%">Shipper Name</th>
                  <th style="width: 33.3%">Shipper Rating</th>
                  <th style="width: 33.3%">Shipper Email</th>
                </tr>
                <tr>
                  <td><router-link :to="{ name: 'shipperProfile', params: { id: post.shipper.id} }" class="fade-on-hover text-blue">{{ post.shipper.name }}</router-link></td>
                  <td>Coming Soon..</td>
                  <td><a :href="'mailto:' + post.shipper.email">{{ post.shipper.email }}</a></td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12 background pt-3">
          <h5>Other Details</h5>
          <hr>
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 25%">Date Posted</th>
                  <th style="width: 25%">Starting Bid</th>
                  <th style="width: 25%">Highest Bid</th>
                  <th style="width: 25%">Current Bid</th>
                </tr>
                <tr>
                  <td>{{ parseDate(post.datePosted) }}</td> 
                  <td>{{ formatMoney(post.startingBid) }}</td>
                  <td>Coming Soon..</td>
                  <td>Coming Soon..</td>
                </tr>
              </table>
              <div v-if="showModal" class ="pt-2 mb-2 col-6 offset-3">
                <div slot="description">
                  Please enter your bid amount
                  <TextInput v-model="bidAmount" placeHolder="bidAmount" errorMessage="Please enter a valid bid amount" :validator="$v.bidAmount" />
                </div>
                <div slot="footer">
                  <button @click="showModal = false" type="button" class="btn btn-secondary m-2">Cancel</button>
                  <button :disabled="$v.bidAmount.$error" type="button" class="btn btn-primary" @click="confirmBid">Bid</button>
                </div>
              </div>
              <button v-if="!showModal && loggedIn" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mb-3" @click="showModal = true">Bid Now!</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import TextInput from '@/components/Form/Input/TextInput.vue'

import postUtilities from '@/utils/postUtilities.js'
import { required, helpers } from 'vuelidate/lib/validators'
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

export default {
  name: 'detailedShipperPost',
  components: {
    TextInput,
  },
  props: {
    id: String
  },
  data() {
    return {
      post: null,
      bidAmount: '',
      error: false,
      bidError: false,
      bidSuccess: false,
      showModal: false,
    }
  },
  methods: {
    getPostById() {
      this.$store.dispatch('posts/getPostById', { type: 'shipper', postId: this.id })
        .then((response) => {
          this.error = false
          this.post = response.data.result
          this.bidAmount = this.post.startingBid.toString()
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
    confirmBid() {
      this.$v.$touch()
      if (this.$v.$invalid) {
				return
      }
      Swal.fire({
        title: 'Are you sure?',
        text: `Are you sure you want to place a bid for $${this.bidAmount}?`,
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, place my bid!'
      }).then((result) => {
        if (result.value) {
          this.submitBid()
        }
      })
    },
    submitBid() {
      this.$store.dispatch('bids/createBid', { type: 'shipper', postId: this.post.id, bid: { bidAmount: this.bidAmount }})
        .then(() => {
          this.showModal = false
          Swal.fire({
            type: 'success',
            title: 'Successfully bid',
            text: 'Bid has successfully been posted!',
          })
        })
        .catch(() => {
          this.showModal = false
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! Please try again!',
          })
        })
    },
    formatAddress(address) {
      return postUtilities.formatAddress(address)
    },
    parseDate(value) {
      return postUtilities.parseDate(value)
    },
    parseVehicleCondition(condition) {
      return postUtilities.parseVehicleCondition(condition)
    },
    formatMoney(money) {
      return postUtilities.formatMoney(money)
    }
  },
  computed: {
    loggedIn() {
      return this.$store.getters['authentication/getAccountType'] != '' 
    },
  },
  validations: {
    bidAmount: {
      required,
      bidRegex,
    },
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.getPostById()
  }
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss";
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
.contact {
  overflow: hidden;
}
</style>