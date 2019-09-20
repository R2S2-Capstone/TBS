<template>
  <div class="container pt-5 pb-5">
    <div class="row" v-if="error">
      <div class="col-12 text-center">
        <h4 class="text-danger mb-5">Failed to load post...</h4>
        <h6 @click="getPostById()" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to try again</h6><br>
        <h6 @click="$router.go(-1)" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to return</h6>
      </div>
    </div>
    <div class="row" v-if="post.id != null">
      <div class="col-lg-8 col-md-9 col-sm-12 mb-5 text-center">
        <div class="col-12 background">
          <div class="row pt-3">
            <div class="col-12">
              <h5>Pickup Details</h5>
              <hr>
              <p></p>
              <div class="row">
                <div class="col-12">
                  <p>Pickup Location: {{ post.pickupLocation }}</p>
                  <p>Pickup Date: {{ post.pickupDate.split('T')[0] }}</p>
                  <p>Pickup Time: {{ convertTime(this.post.pickupDate.split('T')[1]) }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-2">
            <div class="col-12">
              <h5>Dropoff Details</h5>
              <hr>
              <p></p>
              <div class="row">
                <div class="col-12">
                  <p>Dropoff Location: {{ post.dropoffLocation }}</p>
                  <p>Dropoff Date: {{ post.dropoffDate.split('T')[0] }}</p>
                  <p>Dropoff Time: {{ convertTime(this.post.dropoffDate.split('T')[1]) }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-2 mb-3">
            <div class="col-12">
              <h5>Other Details</h5>
              <hr>
              <p></p>
              <div class="row">
                <div class="col-12">
                  <p>Date Posted: {{ post.datePosted.split('T')[0] }}</p>
                  <p>Trailer Type: {{ parseTrailerType(this.post.trailerType) }}</p>
                  <p>Spaces Available: {{ post.spacesAvailable }}</p>
                  <p>Starting Bid: ${{ post.startingBid }}</p>
                  <p>Current highest bid: Coming soon...</p>
                  <p>Current lowest bid: Coming soon...</p>
                  <div v-if="showModal" class ="pt-2 mb-2 border">
                    <div slot="description">
                      Please enter your bid amount
                      <TextInput v-model="bidAmount" placeHolder="bidAmount" errorMessage="Please enter a valid bid amount" :validator="$v.bidAmount" />
                      
                      <div class="row">
                        <div class="col-12 pt-2">
                          <h5>Vehicle Information</h5>
                          <hr>
                          <p></p>
                        </div>
                        <div class="col-12">
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.vehicle.make" placeHolder="Make" errorMessage="Please enter a vehicle make" :validator="$v.bidPost.vehicle.make"/>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.vehicle.model" placeHolder="Model" errorMessage="Please enter a vehicle model" :validator="$v.bidPost.vehicle.model"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                              <label>Year</label>
                              <YearInput v-model="bidPost.vehicle.year" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <ConditionInput v-model="bidPost.vehicle.condition"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.vehicle.vin" placeHolder="VIN" errorMessage="Please enter a valid vin" :validator="$v.bidPost.vehicle.vin"/>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row pt-3">
                        <div class="col-12">
                          <h5>Pickup</h5>
                          <hr>
                          <p></p>
                        </div>
                        <div class="col-12">
                          <div class="row">
                            <div class="col-12">
                              <TextInput v-model="bidPost.pickupLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.bidPost.pickupLocation.addressLine"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.pickupLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.bidPost.pickupLocation.city"/>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <ProvinceInput v-model="bidPost.pickupLocation.province" />
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <CountryInput v-model="bidPost.pickupLocation.country" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.pickupLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.bidPost.pickupLocation.postalCode"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-12">
                              <label>Date</label>
                              <DateInput v-model="bidPost.pickupDate" />
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row pt-3">
                        <div class="col-12">
                          <h5>Delivery</h5>
                          <hr>
                          <p></p>
                        </div>
                        <div class="col-12">
                          <div class="row">
                            <div class="col-12">
                              <TextInput v-model="bidPost.dropoffLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.bidPost.dropoffLocation.addressLine"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.dropoffLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.bidPost.dropoffLocation.city"/>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
                              <ProvinceInput v-model="bidPost.dropoffLocation.province" />
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <CountryInput v-model="bidPost.dropoffLocation.country" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                              <TextInput v-model="bidPost.dropoffLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.bidPost.dropoffLocation.postalCode"/>
                            </div>
                          </div>
                          <div class="row">
                            <div class="col-12">
                              <label>Date</label>
                              <DateInput v-model="bidPost.dropoffDate" />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div slot="footer">
                      <button @click="showModal = false" type="button" class="btn btn-secondary m-2">Cancel</button>
                      <button :disabled="$v.bidAmount.$error" type="button" class="btn btn-primary" @click="confirmBid">Bid</button>
                    </div>
                  </div>
                  <button v-if="!showModal && loggedIn" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="showModal = true">Bid Now!</button>
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
              <p>Company Name:<br>{{ post.carrier.company.name }}</p>
              <p class="contact">
                Contact Name:<br>{{ post.carrier.company.contact.name }}<br>
                Contact Phone Number:<br><a :href="'tel:' + post.carrier.company.contact.phoneNumber">{{ this.post.carrier.company.contact.phoneNumber }}</a><br>
                Contact Email:<br><a :href="'mailto:' + post.carrier.company.contact.email">{{ this.post.carrier.company.contact.email }}</a><br>
              </p>
            </div>
          </div>
        </div>
        <div class="col-12 background mb-5 pt-3">
          <h5>Carrier Details</h5>
          <hr>
          <div class="row">
            <div class="col-12">
              <p></p>
              <p>
                Name:<br>{{ post.carrier.name }}<br>
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
import Swal from 'sweetalert2'

import TextInput from '@/components/Form/Input/TextInput.vue'
import ProvinceInput from '@/components/Form/Input/ProvinceInput.vue'
import CountryInput from '@/components/Form/Input/CountryInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import ConditionInput from '@/components/Form/Input/ConditionInput.vue'
import YearInput from '@/components/Form/Input/YearInput.vue'

import utilities from '@/utils/postUtilities.js'
import { required, helpers } from 'vuelidate/lib/validators'
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)

export default {
  name: 'detailedCarrierPost',
  components: {
    TextInput,
    ProvinceInput,
    CountryInput,
    DateInput,
    ConditionInput,
    YearInput,
  },
  props: {
    id: String
  },
  data() {
    return {
      post: {},
      bidPost: {
        vehicle: {
          year: new Date().getUTCFullYear().toString(),
          make: '',
          model: '',
          VIN: '',
          condition: 'New',
        },
        pickupLocation: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        pickupDate: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0], // This is the one passed to the API and will be a combination of the two fields above
        dropoffLocation: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        dropoffDate: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0], // This is the one passed to the API and will be a combination of the two fields above
      },
      bidAmount: '',
      error: false,
      bidError: false,
      bidSuccess: false,
      showModal: false,
    }
  },
  methods: {
    getPostById() {
      this.$store.dispatch('posts/getPostById', { type: 'carrier', postId: this.id })
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
      this.$store.dispatch('bids/createBid', { type: 'carrier', postId: this.post.id, bid: { bidAmount: this.bidAmount, ...this.bidPost }})
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
    convertTime(value) {
      return utilities.convertTime(value)
    },
    parseTrailerType(trailerType) {
      return utilities.parseTrailerType(trailerType)
    }
  },
  computed: {
    loggedIn() {
      return this.$store.getters['authentication/getAccountType'] != '' 
    }
  },
  validations: {
    bidAmount: {
      required,
      bidRegex,
    },
    bidPost: {
      vehicle: {
        make: {
          required
        },
        model: {
          required
        },
        vin: {
          required
        },
      },
      pickupLocation: {
        addressLine: {
          required
        },
        city: {
          required
        },
        postalCode: {
          required,
          postalCodeRegex
        },
      },
      dropoffLocation: {
        addressLine: {
          required
        },
        city: {
          required
        },
        postalCode: {
          required,
          postalCodeRegex,
        },
      },
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
.contact {
  overflow: hidden;
}
</style>