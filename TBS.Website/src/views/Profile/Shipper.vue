<template>
  <div class="container pt-5 pb-5">
    <div class="row" v-if="error">
      <div class="col-12 text-center">
        <h4 class="text-danger mb-5">Failed to load profile...</h4>
        <h6 @click="fetchProfile()" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to try again</h6><br>
        <h6 @click="$router.go(-1)" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to return</h6>
      </div>
    </div>
    <div v-if="profile">
      <div class="row pt-3 text-center">
        <div class="col-12">
          <h3>{{ profile.name }}</h3>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12 background">
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 50%">Email</th>
                  <th style="width: 50%">Dealer Number</th>
                </tr>
                <tr>
                  <td><a :href="'mailto:' + profile.email">{{ profile.email }}</a></td> 
                  <td>{{ profile.dealerNumber }}</td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12">
          <h3>{{ profile.company.name }}</h3>
          <h6>{{ formatAddress(profile.company.address) }}</h6>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12 background">
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 33.3%"></th>
                  <th style="width: 33.3%">Company Email</th>
                  <th style="width: 33.3%">Company Phone</th>
                </tr>
                <tr>
                  <td>{{ profile.company.contact.name }}</td> 
                  <td><a :href="'mailto:' + profile.company.contact.email">{{ profile.company.contact.email }}</a></td> 
                  <td><a :href="'tel:' + profile.company.contact.phoneNumber">{{ profile.company.contact.phoneNumber }}</a></td> 
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12">
          <h3 v-if="reviews.length != 0">Reviews</h3>
          <h3 v-else>No Reviews</h3>
        </div>     
      </div>
      <div class="row pt-3" v-if="reviews.length != 0">
        <div class="col-12 background">
          <div class="row pt-3">
            <div class="col-12 text-center" v-for="(review, index) in reviews" :key="index">
              <p>{{ review.carrier.name }} | {{`${parseDate(review.reviewDate)}` }}</p>
              <p><star-rating style="display: inline-block;" :increment="0.5" :show-rating=false :read-only=true :star-size=30 v-model="review.rating"></star-rating></p>
              <p>{{ review.review }}</p>
              <hr v-if="reviews.length - 1 != index" class="pt-3">
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import postUtilities from '@/utils/postUtilities.js'
import StarRating from 'vue-star-rating'
export default {
  name: 'detailedCarrierPost',
  components: {
    StarRating
  },
  props: {
    id: String
  },
  data() {
    return {
      profile: null,
      reviews: [],
      error: false,
    }
  },
  methods: {
    parseTrailerType(trailerType) {
      return postUtilities.parseTrailerType(trailerType)
    },
     parseDate(value) {
      return postUtilities.parseDate(value)
    },
    formatAddress(address) {
      return postUtilities.formatAddress(address)
    },
    fetchProfile() {
      this.$store.dispatch('profiles/getProfileById', {profileId: this.id, type: 'shipper'})
        .then((response) => {
          this.profile = response.data.result
          this.reviews = this.profile.reviews
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
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.fetchProfile()
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
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>