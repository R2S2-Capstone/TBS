<template>
  <div class="container pt-5">
    <Back/>
    <WideFormCard :title="type + ' Post'">
      <div slot="card-information" class="text-center">
        <h4 v-if="error" class="text-danger pb-4">Failed to {{ type.toLowerCase()}} post</h4>
        <h4 v-if="deleteError" class="text-danger pb-4">Failed to delete post</h4>
      </div>
      <div slot="card-content" class="text-center">
        <form @submit.prevent="submit">
          <div class="row">
            <div class="col-12">
              <h5>Vehicle Information</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.vehicle.make" placeHolder="Make" errorMessage="Please enter a vehicle make" :validator="$v.post.vehicle.make"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.vehicle.model" placeHolder="Model" errorMessage="Please enter a vehicle model" :validator="$v.post.vehicle.model"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Year</label>
                  <YearInput v-model="post.vehicle.year" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <ConditionInput v-model="post.vehicle.condition"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.vehicle.vin" placeHolder="VIN" errorMessage="Please enter a valid vin" :validator="$v.post.vehicle.vin"/>
                </div>
              </div>
                  <!-- TODO: Add more vehicle information? -->
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Pickup</h5>
              <hr>
            </div>
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
                <div class="col-12">
                  <label>Date</label>
                  <DateInput v-model="post.pickupDate" />
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.pickupContact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.post.pickupContact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">                  
                  <TextInput v-model="post.pickupContact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.post.pickupContact.phoneNumber"/>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <EmailInput v-model="post.pickupContact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.post.pickupContact.email"/>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Delivery</h5>
              <hr>
            </div>
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
                <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
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
              <div class="row">
                <div class="col-12">
                  <label>Date</label>
                  <DateInput v-model="post.dropoffDate" />
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.dropoffContact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.post.dropoffContact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="post.dropoffContact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.post.dropoffContact.phoneNumber"/>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <EmailInput v-model="post.dropoffContact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.post.dropoffContact.email"/>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Other Details</h5>
              <hr>
            </div>
            <div class="col-12">
              <TextInput v-model="post.startingBid" placeHolder="Starting bid" errorMessage="Please enter a valid starting bid" :validator="$v.post.startingBid"/>
            </div>
          </div>
        </form>
      </div>
      <div slot="below-form" class="text-center">
        <div class="col-12 pt-2" v-if="type == 'Edit'">
          <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" @click="deletePost()">Delete</button>
        </div>
        <div class="col-12">
          <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" @click="submit" type="submit">{{ type }}</button>
        </div>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import Back from '@/components/Back.vue'
import WideFormCard from '@/components/Form/Card/WideFormCard.vue'

import TextInput from '@/components/Form/Input/TextInput.vue'
import EmailInput from '@/components/Form/Input/EmailInput.vue'
import ProvinceInput from '@/components/Form/Input/ProvinceInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import ConditionInput from '@/components/Form/Input/ConditionInput.vue'
import CountryInput from '@/components/Form/Input/CountryInput.vue'
import YearInput from '@/components/Form/Input/YearInput.vue'

import { required, helpers, email } from 'vuelidate/lib/validators'
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

export default {
  name: 'shipperCreatePost',
  components: {
    Back,
    WideFormCard,
    TextInput,
    EmailInput,
    ProvinceInput,
    DateInput,
    ConditionInput,
    CountryInput,
    YearInput,
  },
  data() {
    return {
      error: false,
      deleteError: false,
      post: {
        id: '',
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
        pickupContact: {
          name: '',
          email: '',
          phoneNumber: '',
        },
        dropoffLocation: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        // TODO: Set the minimum for this date to be the same date as pickup
        dropoffDate: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0], // This is the one passed to the API and will be a combination of the two fields above
        dropoffContact: {
          name: '',
          email: '',
          phoneNumber: '',
        },
        startingBid: ''
      }
    }
  },
  validations: {
    post: {
      vehicle: {
        make: {
          required
        },
        model: {
          required
        },
        vin: {
          // required
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
      pickupContact: {
        name: {
          required
        },
        email: {
          required,
          email
        },
        phoneNumber: {
          required,
          phoneNumberRegex,
        }
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
      dropoffContact: {
        name: {
          required
        },
        email: {
          required,
          email
        },
        phoneNumber: {
          required,
          phoneNumberRegex,
        }
      },
      startingBid: {
        required,
        bidRegex
      }
    }
  },
  methods: {
    submit() {
      this.$v.$touch()
      if (this.$v.$invalid) {
				return;
      }
      // Will either be 'posts/createPost' or 'posts/updatePost'
      this.$store.dispatch(`posts/${this.type.toLowerCase()}Post`, this.post)
				.then(() => {
          // TODO: Go to posted page
          this.$router.push({name: 'shipperHome' })
				})
				.catch(() => {
					this.error = true
				})
    },
    deletePost() {
        this.$store.dispatch('posts/deletePost', this.post.id)
          .then(() => {
            // TODO: Go to delete confirmation page
            this.$router.push({name: 'shipperHome' })
          })
          .catch(() => {
            this.deleteError = true
          })
    }
  },
  computed: {
    type() {
      if (this.$route.params.id != null) {
        return 'Update'
      } else {
        return 'Create'
      }
    }
  },
  created() {
    if (this.type == 'Update') {
      // Load post here
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
