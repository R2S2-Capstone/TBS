<template>
  <div class="container pt-5">
    <Back/>
    <WideFormCard :title="type + ' Post'">
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
                  <TextInput v-model="vehicle.make" placeHolder="Make" errorMessage="Please enter a vehicle make" :validator="$v.vehicle.make"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="vehicle.model" placeHolder="Make" errorMessage="Please enter a vehicle model" :validator="$v.vehicle.model"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Year</label>
                  <YearInput v-model="vehicle.year" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <ConditionInput v-model="vehicle.condition"/>
                </div>
                  <!-- TODO: Add more vehicle information? -->
              </div>
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
                  <TextInput v-model="pickupLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.pickupLocation.addressLine"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="pickupLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.pickupLocation.city"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <ProvinceInput v-model="pickupLocation.province" />
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <CountryInput v-model="pickupLocation.country" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="pickupLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.pickupLocation.postalCode"/>
                </div>
              </div>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Date</label>
                  <DateInput v-model="pickupDateValue" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Time</label>
                  <TimeInput v-model="pickupTime" />
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="pickupContact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.pickupContact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">                  
                  <TextInput v-model="pickupContact.phone" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.pickupContact.phone"/>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <EmailInput v-model="pickupContact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.pickupContact.email"/>
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
                  <TextInput v-model="dropoffLocation.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.dropoffLocation.addressLine"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoffLocation.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.dropoffLocation.city"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
                  <ProvinceInput v-model="dropoffLocation.province" />
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <CountryInput v-model="dropoffLocation.country" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoffLocation.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.dropoffLocation.postalCode"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Date</label>
                  <DateInput v-model="dropoffDateValue" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Time</label>
                  <TimeInput v-model="dropoffTime" />
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoffContact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.dropoffContact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoffContact.phone" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.dropoffContact.phone"/>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <EmailInput v-model="dropoffContact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.dropoffContact.email"/>
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
              <TextInput v-model="startingBid" placeHolder="Starting bid" errorMessage="Please enter a valid starting bid" :validator="$v.startingBid"/>
            </div>
          </div>
        </form>
      </div>
      <div slot="below-form" class="text-center">
        <div class="col-12 pt-2" v-if="type == 'Edit'">
          <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" @click="deletePost()">Delete</button>
        </div>
        <div class="col-12">
          <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" type="submit">{{ type }}</button>
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
import TimeInput from '@/components/Form/Input/TimeInput.vue'
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
    TimeInput,
    ConditionInput,
    CountryInput,
    YearInput,
  },
  data() {
    return {
      date: new Date(),
      vehicle: {
        year: '2019',
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
      pickupDateValue: '',
      pickupTime: '',
      pickupDate: '', // This is the one passed to the API and will be a combination of the two fields above
      pickupContact: {
        name: '',
        email: '',
        phone: '',
      },
      dropoffLocation: {
        addressLine: '',
        city: '',
        province: 'Ontario',
        country: 'Canada',
        postalCode: '',
      },
      dropoffDateValue: '',
      dropoffTime: '',
      dropoff: '', // This is the one passed to the API and will be a combination of the two fields above
      dropoffContact: {
        name: '',
        email: '',
        phone: '',
      },
      startingBid: ''
    }
  },
  validations: {
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
    pickupContact: {
      name: {
        required
      },
      email: {
        required,
        email
      },
      phone: {
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
        phoneNumberRegex,
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
      phone: {
        required,
        phoneNumberRegex,
      }
    },
    startingBid: {
      required,
      bidRegex
    }
  },
  methods: {
    submit() {
      // TODO: Submit
      this.$router.push({ name: 'shipperHome' })
    },
    deletePost() {
      this.$router.push({ name: 'shipperHome' })
    }
  },
  computed: {
    type() {
      if (this.$route.params.id != null) {
        return 'Edit'
      } else {
        return 'Create'
      }
    }
  },
  created() {
    if (this.type == 'Edit') {
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
