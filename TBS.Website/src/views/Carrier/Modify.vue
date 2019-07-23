<template>
  <div class="container pt-5">
    <Back/>
    <WideFormCard :title="type + ' Availability Post'">
      <div slot="card-information" class="text-center">
        <h4 v-if="error" class="text-danger pb-4">Failed to {{ type.toLowerCase()}} post</h4>
        <h4 v-if="deleteError" class="text-danger pb-4">Failed to delete post</h4>
        <h4 v-if="failedToLoadError" class="text-danger pb-4">Failed to load post</h4>
      </div>
      <div slot="card-content" class="text-center">
        <form @submit.prevent="submit">
          <div class="row">
            <div class="col-12">
              <h5>Pickup Information</h5>
              <hr>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="row">
                  <div class="col-12">
                    <TextInput v-model="post.pickupLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.post.pickupLocation.addressLine"/>
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <TextInput v-model="post.pickupLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.post.pickupLocation.city"/>
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <ProvinceInput v-model="post.pickupLocation.province" />
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <CountryInput v-model="post.pickupLocation.country" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <TextInput v-model="post.pickupLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.post.pickupLocation.postalCode"/>
                  </div>
                </div>
              </div>
              <div class="col-12">
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Date</label>
                    <DateInput v-model="post.pickupDateValue" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Time</label>
                    <TimeInput v-model="post.pickupTime" />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-5">
            <div class="col-12">
              <h5>Delivery Information</h5>
              <hr>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="row">
                  <div class="col-12">
                    <TextInput v-model="post.dropoffLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.post.dropoffLocation.addressLine"/>
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <TextInput v-model="post.dropoffLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.post.dropoffLocation.city"/>
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <ProvinceInput v-model="post.dropoffLocation.province" />
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <CountryInput v-model="post.dropoffLocation.country" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <TextInput v-model="post.dropoffLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.post.dropoffLocation.postalCode"/>
                  </div>
                </div>
              </div>
              <div class="col-12">
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Date</label>
                    <DateInput v-model="post.dropoffDateValue" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Time</label>
                    {{ this.post.dropoffTime }}
                    <TimeInput v-model="post.dropoffTime" />
                  </div>
                </div>
              </div>
            </div>
            <div class="row pt-5">
              <div class="col-12">
                <h5>Other Details</h5>
                <hr>
              </div>
              <div class="col-12">
                <TextInput v-model="post.startingBid" placeHolder="Starting Bid" errorMessage="Please enter a starting bid" :validator="this.$v.post.startingBid" />
              </div>
              <div class="col-12">
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Trailer Type</label>
                    <TrailerInput v-model="post.trailerType" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Spaces Available</label>
                    <CapacityInput v-model="post.spacesAvailable" />
                  </div>
                </div>
              </div>
              <div class="col-12">
                <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" type="submit">{{ type }}</button>
              </div>
              <div class="col-12 pt-2" v-if="type == 'Edit'">
                <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="deletePost()">Delete</button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import Back from '@/components/Back.vue'
import WideFormCard from '@/components/Form/Card/WideFormCard.vue'

import TextInput from '@/components/Form/Input/TextInput.vue'
import ProvinceInput from '@/components/Form/Input/ProvinceInput.vue'
import CountryInput from '@/components/Form/Input/CountryInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import TimeInput from '@/components/Form/Input/TimeInput.vue'
import CapacityInput from '@/components/Form/Input/CapacityInput.vue'
import TrailerInput from '@/components/Form/Input/TrailerInput.vue'

import { required, helpers } from 'vuelidate/lib/validators'
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

import utilities from '@/utils/postUtilities.js'

export default {
  name: 'carrierCreatePost',
  components: {
    Back,
    WideFormCard,
    TextInput,
    ProvinceInput,
    CountryInput,
    DateInput,
    TimeInput,
    CapacityInput,
    TrailerInput,
  },
  data() {
    return {
      error: false,
      deleteError: false,
      failedToLoadError: false,
      post: {
        pickupLocation: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        pickupDate: '', 
        pickupDateValue: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0],
        pickupTime: new Date().getHours() + ":" + new Date().getMinutes(),
        dropoffLocation: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        dropoffDate: '',
        dropoffDateValue: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0],
        dropoffTime: new Date().getHours() + ":" + new Date().getMinutes(),
        trailerType: 'Car Carrier',
        spacesAvailable: 1,
        startingBid: '',
      }
    }
  },
  validations: {
    post: {
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
      startingBid:{
        required,
        bidRegex,
      },
    },
  },
  methods: {
    submit() {
      this.$v.$touch()
      if (this.$v.$invalid) {
				return
      }
      this.post.pickupDate = `${this.post.pickupDateValue} ${this.post.pickupTime}`
      this.post.dropoffDate = `${this.post.dropoffDateValue} ${this.post.dropoffTime}`
      // Will either be 'posts/createPost' or 'posts/updatePost'
      this.$store.dispatch(`posts/${this.type.toLowerCase()}Post`, { pickupLocation: this.post.pickupLocation, pickupDate: this.post.pickupDate, dropoffLocation: this.post.dropoffLocation, dropoffDate: this.post.dropoffDate, spacesAvailable: this.post.spacesAvailable, startingBid: this.post.startingBid })
				.then(() => {
          // TODO: Go to posted page
          this.$router.push({name: 'carrierHome' })
				})
				.catch(() => {
					this.error = true
				})
    },
    deletePost() {
      this.$router.push({ name: 'carrierHome' })
    }
  },
  computed: {
    type() {
      if (this.$route.params.id != null) {
        return 'Edit'
      } else {
        return 'Create'
      }
    },
  },
  created() {
    if (this.type == 'Edit') {
      this.$store.dispatch('posts/getPostById', { postId: this.$route.params.id })
				.then((response) => {
          this.post = response.data.result
          this.post.startingBid = this.post.startingBid.toString()
          this.post.trailerType = utilities.parseTrailerType(this.post.trailerType)
          this.post.pickupDateValue = this.post.pickupDate.split('T')[0]
          this.post.pickupTime = this.post.pickupDate.split('T')[1].substring(0, 5)
          this.post.dropoffDateValue = this.post.dropoffDate.split('T')[0]
          this.post.dropoffTime = this.post.dropoffDate.split('T')[1].substring(0, 5)
          this.failedToLoadError = false
				})
				.catch(() => {
					this.failedToLoadError = true
				})
    }
  }
}
</script>

<style lang="scss" scoped>
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>