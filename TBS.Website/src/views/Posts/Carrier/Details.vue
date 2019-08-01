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
                  <p>Pickup Location: {{ this.post.pickupLocation }}</p>
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
                  <p>Dropoff Location: {{ this.post.dropoffLocation }}</p>
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
                  <p>Date Posted: {{ this.post.datePosted.split('T')[0] }}</p>
                  <p>Trailer Type: {{ parseTrailerType(this.post.trailerType) }}</p>
                  <p>Spaces Available: {{ this.post.spacesAvailable }}</p>
                  <p>Starting Bid: ${{ this.post.startingBid }}</p>
                  <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Bid Now!</button>
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
              <p>Company Name:<br>{{ this.post.carrier.company.name }}</p>
              <p>
                Contact Name:<br>{{ this.post.carrier.company.contact.name }}<br>
                Contact Phone Number:<br><a :href="'tel:' + this.post.carrier.company.contact.phoneNumber">{{ this.post.carrier.company.contact.phoneNumber }}</a><br>
                Contact Email:<br><a :href="'mailto:' + this.post.carrier.company.contact.email">{{ this.post.carrier.company.contact.email }}</a><br>
              </p>
            </div>
          </div>
        </div>
        <div class="col-12 background mb-5 pt-3">
          <h5>Carrier Details</h5>
          <hr>
          <div class="row">
            <div class="col-12">
              <p>
                Name:<br>{{ this.post.carrier.name }}<br>
                Rating:<br>Coming Soon!<br>
                Number of Deliveries:<br>Coming Soon!<br>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import utilities from '@/utils/postUtilities.js'

export default {
  name: 'detailedCarrierPost',
  data() {
    return {
      post: null,
      error: false,
    }
  },
  props: {
    id: String
  },
  methods: {
    getPostById() {
      this.$store.dispatch('posts/getPostById', { type: 'carrier', postId: this.id })
        .then((response) => {
          this.error = false
          this.post = response.data.result
        })
        .catch((error) => {
          this.error = true
        })
    },
    convertTime(value) {
      return utilities.convertTime(value)
    },
    parseTrailerType(trailerType) {
      return utilities.parseTrailerType(trailerType)
    }
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