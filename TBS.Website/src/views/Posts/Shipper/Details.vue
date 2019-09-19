<template>
  <div class="container pt-5">
    <div class="row" v-if="error">
      <div class="col-12 text-center">
        <h4 class="text-danger mb-5">Failed to load post...</h4>
        <h6 @click="getPostById()" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to try again</h6><br>
        <h6 @click="$router.go(-1)" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to return</h6>
      </div>
    </div>
    <div class="row" v-if="!error && post != null">
      <div class="col-lg-8 col-md-9 col-sm-12 mb-3 text-center">
        <div class="col-12 background">
          <div class="row pt-3">
            <div class="col-12">
              <h3>{{ `${post.vehicle.year} ${post.vehicle.make} ${post.vehicle.model}` }}</h3>
              <hr>
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Pickup Details</h5>
              <hr>
              <div class="row">
                <div class="col-12">
                  <p></p>
                  <p>Location: {{ formatAddress(post.pickupLocation) }}</p>
                  <p>Date: {{ post.pickupDate.split('T')[0] }}</p>
                  <p>Time: {{ convertTime(post.pickupDate.split('T')[1]) }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-2">
            <div class="col-12">
              <h5>Delivery Details</h5>
              <hr>
              <div class="row">
                <div class="col-12">
                  <p></p>
                  <p>Location: {{ formatAddress(post.dropoffLocation) }}</p>
                  <p>Date: {{ post.dropoffDate.split('T')[0] }}</p>
                  <p>Time: {{ convertTime(post.dropoffDate.split('T')[1]) }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-2 mb-3">
            <div class="col-12">
              <h5>Other Details</h5>
              <hr>
              <div class="row">
                <div class="col-12">
                  <p></p>
                  <p>Date Posted: {{ post.datePosted.split('T')[0] }}</p>
                  <p>Starting Bid: ${{ post.startingBid }}</p>
                  <p>Current highest bid: Coming soon...</p>
                  <p>Current lowest bid: Coming soon...</p>
                  <div v-if="showModal" class ="pt-2 mb-2 col-6 offset-3 border">
                    <div slot="description">
                      Please enter your bid amount
                      <TextInput v-model="bidAmount" placeHolder="bidAmount" errorMessage="Please enter a valid bid amount" :validator="$v.bidAmount" />
                    </div>
                    <div slot="footer">
                      <button @click="showModal = false" type="button" class="btn btn-secondary m-2">Cancel</button>
                      <button :disabled="$v.bidAmount.$error" type="button" class="btn btn-primary" @click="confirmBid">Bid</button>
                    </div>
                  </div>
                  <button v-if="!showModal" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="showModal = true">Bid Now!</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-md-3 col-sm-12 text-center">
        <div class="col-12 background mb-5 pt-3">
          <h5>Company Information</h5>
          <hr>
          <div class="row">
            <div class="col-12">
              <p></p>
              <p>Company Name:<br>{{ post.shipper.company.name }}</p>
              <p class="contact">
                Contact Name:<br>{{ post.shipper.company.contact.name }}<br>
                Contact Phone Number:<br><a :href="'tel:' + post.shipper.company.contact.phoneNumber">{{ post.shipper.company.contact.phoneNumber }}</a><br>
                Contact Email:<br><a :href="'mailto:' + post.shipper.company.contact.email">{{ post.shipper.company.contact.email }}</a><br>
              </p>
            </div>
          </div>
        </div>
        <div class="col-12 background mb-5 pt-3">
          <h5>Shipper Details</h5>
          <hr>
          <div class="row">
            <div class="col-12">
              <p></p>            
              <p>
                Name:<br>{{ post.shipper.name }}<br>
                Rating:<br>Coming Soon!<br>
              </p>
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

import utilities from '@/utils/postUtilities.js'
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
      return utilities.formatAddress(address)
    },
    convertTime(value) {
      return utilities.convertTime(value)
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