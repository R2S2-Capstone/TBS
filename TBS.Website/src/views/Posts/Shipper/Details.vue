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
              <h5>Pickup Details</h5>
              <hr>
              <div class="row">
                <div class="col-12">
                  <p></p>
                  <p>Pickup Location: {{ formatAddress(this.post.pickupLocation) }}</p>
                  <p>Pickup Date: {{ this.post.pickupDate.split('T')[0] }}</p>
                  <p>Pickup Time: {{ convertTime(this.post.pickupDate.split('T')[1]) }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-2">
            <div class="col-12">
              <h5>Dropoff Details</h5>
              <hr>
              <div class="row">
                <div class="col-12">
                  <p></p>
                  <p>Dropoff Location: {{ formatAddress(this.post.dropoffLocation) }}</p>
                  <p>Dropoff Date: {{ this.post.dropoffDate.split('T')[0] }}</p>
                  <p>Dropoff Time: {{ convertTime(this.post.dropoffDate.split('T')[1]) }}</p>
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
                  <p>Date Posted: {{ this.post.datePosted.split('T')[0] }}</p>
                  <p>Starting Bid: ${{ this.post.startingBid }}</p>
                  <p>Current highest bid: Coming soon...</p>
                  <p>Current lowest bid: Coming soon...</p>
                  <div v-if="showModal" class ="pt-2 pb-2 col-6 offset-3 border">
                    <div slot="description">
                      Please enter your bid amount
                      <TextInput v-model="bidAmount" placeHolder="bidAmount" errorMessage="Please enter a valid bid amount" :validator="$v.bidAmount" />
                    </div>
                    <div slot="footer">
                      <button @click="showModal = false" type="button" class="btn btn-secondary m-2">Cancel</button>
                      <button :disabled="$v.bidAmount.$error" type="button" class="btn btn-primary" @click="submitBid">Bid</button>
                    </div>
                  </div>
                  <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="showModal = true">Bid Now!</button>
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
              <p>Company Name:<br>{{ this.post.shipper.company.name }}</p>
              <p>
                Contact Name:<br>{{ this.post.shipper.company.contact.name }}<br>
                Contact Phone Number:<br><a :href="'tel:' + this.post.shipper.company.contact.phoneNumber">{{ this.post.shipper.company.contact.phoneNumber }}</a><br>
                Contact Email:<br><a :href="'mailto:' + this.post.shipper.company.contact.email">{{ this.post.shipper.company.contact.email }}</a><br>
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
                Name:<br>{{ this.post.shipper.name }}<br>
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
          this.error = true
        })
    },
    submitBid() {
      console.log('asd')
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
</style>