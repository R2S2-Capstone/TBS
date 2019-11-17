<template>
  <div class="container pt-5 pb-5">
    <div class="row" v-if="error">
      <div class="col-12 text-center">
        <h4 class="text-danger mb-5">Failed to load post...</h4>
        <h6 @click="getPostById()" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to try again</h6><br>
        <h6 @click="$router.go(-1)" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white">Click here to return</h6>
      </div>
    </div>
    <div v-if="post.id != null">
      <div class="row pt-3 text-center">
        <div class="col-12 background pt-3">
          <h5>Post Details</h5>
          <hr>
          <p></p>
          <div class="row">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 33%"></th>
                  <th style="width: 33%">City</th>
                  <th style="width: 33%">Date</th>
                </tr>
                <tr>
                  <th>Pickup In: </th>
                  <td>{{ post.pickupLocation }}</td>
                  <td>{{ `${parseDate(post.pickupDate)}` }}</td>
                </tr>
                <tr>
                  <th>Dropoff To: </th>
                  <td>{{ post.dropoffLocation }}</td>
                  <td>{{ `${parseDate(post.dropoffDate)}` }}</td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3 text-center">
        <div class="col-12 background pt-3">
          <h5>Carrier Information</h5>
          <hr>
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 33.3%">Carrier Name</th>
                  <th style="width: 33.3%">Carrier Rating</th>
                  <th style="width: 33.3%">Carrier Email</th>
                </tr>
                <tr>
                  <td><router-link :to="{ name: 'carrierProfile', params: {id:post.carrier.id}}" class="fade-on-hover text-blue">{{ post.carrier.name }}</router-link></td>
                  <td><star-rating :inline=true :show-rating=false :increment="0.5" :read-only=true :star-size=30 v-model="reviewScore"></star-rating></td>
                  <td><a :href="'mailto:' + post.carrier.email">{{ post.carrier.email }}</a></td>
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
                      <th style="width: 25%">Carrier Vehicle</th>
                      <th style="width: 25%">Spaces Available</th>
                      <th style="width: 25%">Trailer Type</th>
                    </tr>
                    <tr>
                      <td>{{ `${post.carrier.vehicle.year} ${post.carrier.vehicle.make} ${post.carrier.vehicle.model}` }}</td> 
                      <td>{{ post.spacesAvailable }}</td>
                      <td>{{ parseTrailerType(post.trailerType) }}</td>
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
          <h5>Other Details</h5>
          <hr>
          <div class="row pt-3">
            <div class="col-12">
              <table class="table">
                <tr>
                  <th style="width: 33.3%">Date Posted</th>
                  <th style="width: 33.3%">Minimum Bid</th>
                  <th style="width: 33.3%">Highest Bid</th>
                </tr>
                <tr>
                  <td>{{ parseDate(post.datePosted) }}</td> 
                  <td>{{ formatMoney(post.startingBid) }}</td>
                  <td>{{ highestBid == null ? 'No bids' : formatMoney(highestBid) }}</td>
                </tr>
              </table>
            </div>
          </div>
          <div class="row pt-3 text-center">
            <div v-if="toShowModal" class ="col-12">
              <div slot="description">
                <TextInput v-model="bidAmount" placeHolder="Bid Amount" errorMessage="Please enter a valid bid amount" :validator="$v.bidAmount" />
                
                <div class="row">
                  <div class="col-12 pt-2">
                    <h5>Vehicle Information</h5>
                    <hr>
                    <p></p>
                  </div>
                  <div class="col-12">
                    <div class="row">
                      <div class="col-lg-6 col-md-6 col-sm-12 pt-2">
                        <TextInput v-model="bidPost.vehicle.make" placeHolder="Make" errorMessage="Please enter a vehicle make" :validator="$v.bidPost.vehicle.make"/>
                      </div>
                      <div class="col-lg-6 col-md-6 col-sm-12 pt-2">
                        <TextInput v-model="bidPost.vehicle.model" placeHolder="Model" errorMessage="Please enter a vehicle model" :validator="$v.bidPost.vehicle.model"/>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-lg-6 col-md-6 col-sm-12 pt-2">
                        <YearInput v-model="bidPost.vehicle.year" />
                      </div>
                      <div class="col-lg-6 col-md-6 col-sm-12 pt-2">
                        <ConditionInput v-model="bidPost.vehicle.condition"/>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-lg-6 col-md-6 col-sm-12 pt-2">
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
                        <div class="form-label-group pt-2">
                          <label>Address</label>
                          <input id="PickupAddress" v-model="bidPost.pickupLocation.addressLine" :class="{ 'is-invalid': validPickupAddress == false }" type="text" class="form-control" placeholder="Pickup Address" />
                          <p v-if="validPickupAddress == false" class="text-danger text-center">Please enter a valid pickup address</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-12">
                        <DateInput v-model="bidPost.pickupDate" class="pt-2" />
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
                  <div class="col-12 pb-2">
                    <div class="row">
                      <div class="col-12">
                        <div class="form-label-group pt-2">
                          <label>Address</label>
                          <input id="DropoffAddress" v-model="bidPost.dropoffLocation.addressLine"  :class="{ 'is-invalid': validDropoffAddress == false }" type="text" class="form-control" placeholder="Dropoff Address" />
                          <p v-if="validDropoffAddress == false" class="text-danger text-center">Please enter a valid dropoff address</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-12">
                        <DateInput v-model="bidPost.dropoffDate" class="pt-2" />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div>
                <button @click="showModal(false)" type="button" class="btn btn-secondary m-2">Cancel</button>
                <button :disabled="$v.bidAmount.$error" type="button" class="btn btn-primary" @click="confirmBid">Bid</button>
              </div>
            </div>
          </div>
          <button v-if="!toShowModal && loggedIn" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white mt-0 mb-3" @click="showModal(true)">Bid Now!</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import TextInput from '@/components/Form/Input/TextInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import ConditionInput from '@/components/Form/Input/ConditionInput.vue'
import YearInput from '@/components/Form/Input/YearInput.vue'
import StarRating from "vue-star-rating";
import postUtilities from '@/utils/postUtilities.js'
import { required, helpers } from 'vuelidate/lib/validators'
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

export default {
  name: 'detailedCarrierPost',
  components: {
    TextInput,
    DateInput,
    ConditionInput,
    YearInput,
    StarRating,
  },
  props: {
    id: String
  },
  data() {
    return {
      post: {},
      highestBid: null,
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
          province: '',
          country: '',
          postalCode: '',
        },
        pickupDate: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0], // This is the one passed to the API and will be a combination of the two fields above
        dropoffLocation: {
          addressLine: '',
          city: '',
          province: '',
          country: '',
          postalCode: '',
        },
        dropoffDate: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0], // This is the one passed to the API and will be a combination of the two fields above
      },
      bidAmount: '',
      error: false,
      bidError: false,
      bidSuccess: false,
      toShowModal: false,
      validPickupAddress: null,
      validDropoffAddress: null,
      reviewScore: 0,
    }
  },
  methods: {
    getPostById() {
      this.$store.dispatch('posts/getPostById', { type: 'carrier', postId: this.id })
        .then((response) => {
          this.error = false
          this.post = response.data.result
          this.bidAmount = this.post.startingBid.toString()
          if (this.post.bids.length != 0) {
            let min = this.post.bids[0].bidAmount, max = this.post.bids[0].bidAmount;
            for (let i = 1, len = this.post.bids.length; i < len; i++) {
              let v = this.post.bids[i].bidAmount;
              min = (v < min) ? v : min;
              max = (v > max) ? v : max;
            }
            this.highestBid = max
          }
          let reviews = this.post.carrier.reviews
          this.reviewScore = 0
          if (reviews !== null) {            
            var totalReviews = 0
            reviews.forEach(review => {
              this.reviewScore += review.rating
              totalReviews += 1
            })
            this.reviewScore = this.reviewScore / totalReviews
          }
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
          this.toShowModal = false
          Swal.fire({
            type: 'success',
            title: 'Successfully bid',
            text: 'Bid has successfully been posted!',
          })
          this.getPostById()
        })
        .catch(() => {
          this.toShowModal = false
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! Please try again!',
          })
        })
    },
    parseDate(value) {
      return postUtilities.parseDate(value)
    },
    parseTime(value) {
      return postUtilities.parseTime(value)
    },
    parseTrailerType(trailerType) {
      return postUtilities.parseTrailerType(trailerType)
    },
    formatMoney(money) {
      return postUtilities.formatMoney(money)
    },
    parsePickupLocationAddress(address) {
      try {
        // TODO FIX
        this.bidPost.pickupLocation.addressLine = `${address.find(a => a.types.includes("street_number")).short_name} ${address.find(a => a.types.includes("route")).long_name}`
        this.bidPost.pickupLocation.city = address.find(a => a.types.includes("locality")).long_name
        this.bidPost.pickupLocation.province = address.find(a => a.types.includes("administrative_area_level_1")).short_name
        this.bidPost.pickupLocation.country = address.find(a => a.types.includes("country")).long_name
        this.bidPost.pickupLocation.postalCode = address.find(a => a.types.includes("postal_code")).long_name
        this.validPickupAddress = true
      } catch {
        this.validPickupAddress = false
      }
    },
    parseDropoffLocationAddress(address) {
      try {
        this.bidPost.dropoffLocation.addressLine = `${address[0].long_name} ${address[1].long_name}`
        this.bidPost.dropoffLocation.city = address[2].long_name
        this.bidPost.dropoffLocation.province = address[4].short_name
        this.bidPost.dropoffLocation.country = address[5].long_name
        this.bidPost.dropoffLocation.postalCode = address[6].long_name
        this.validDropoffAddress = true
      } catch {
        this.validDropoffAddress = false
      }
    },
    showModal(value) {
      this.toShowModal = value
      if (value) {
        setTimeout(() => {
          // eslint-disable-next-line
          var pickupLocation = new google.maps.places.Autocomplete(document.getElementById('PickupAddress'))
          // eslint-disable-next-line
          google.maps.event.addListener(pickupLocation, 'place_changed', () => {
            if (pickupLocation.getPlace().address_components == null) {
              this.validPickupAddress = false
              return
            }
            this.parsePickupLocationAddress(pickupLocation.getPlace().address_components)
          })

          // eslint-disable-next-line
          var dropoffLocation = new google.maps.places.Autocomplete(document.getElementById('DropoffAddress'))
          // eslint-disable-next-line
          google.maps.event.addListener(dropoffLocation, 'place_changed', () => {
            if (dropoffLocation.getPlace().address_components == null) {
              this.validDropoffAddress = false
              return
            }
            this.parseDropoffLocationAddress(dropoffLocation.getPlace().address_components)
          })
        }, 1) // delay for one millisecond so the DOM can be updated
        this.$store.dispatch('profiles/getMyProfile')
        .then((response) => {
          this.bidPost.dropoffLocation = response.data.result.company.address
          this.bidPost.dropoffLocation.id = undefined
        })
      }
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
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>