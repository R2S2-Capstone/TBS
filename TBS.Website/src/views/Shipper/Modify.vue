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
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <TextInput v-model="vehicle.make" placeHolder="Make" errorMessage="Please enter a vehicle make" :validator="$v.vehicle.make"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <TextInput v-model="vehicle.model" placeHolder="Make" errorMessage="Please enter a vehicle model" :validator="$v.vehicle.model"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <select v-model="vehicle.year" class="form-control text-center">
                    <option v-for="(value, index) in years()" :key="index" :value="value" selected>
                      {{ value }}
                    </option>
                  </select>
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
                  <TextInput v-model="pickup.location.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.pickup.location.addressLine"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="pickup.location.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.pickup.location.city"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
                  <ProvinceInput v-model="pickup.location.province" />
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <select v-model="pickup.location.country" class="form-control text-center">
                    <option value="Canada">Canada</option>
                    <option value="USA">USA</option>
                  </select>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="pickup.location.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.pickup.location.postalCode"/>
                </div>
              </div>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Time</label>
                  <input type="text" class="form-control" placeholder="08:00AM">
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <TextInput v-model="pickup.contact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.pickup.contact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <EmailInput v-model="pickup.contact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.pickup.contact.email"/>
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
                  <TextInput v-model="dropoff.location.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.dropoff.location.addressLine"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoff.location.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.dropoff.location.city"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
                  <ProvinceInput v-model="dropoff.location.province" />
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <select v-model="dropoff.location.country" class="form-control text-center">
                    <option value="Canada">Canada</option>
                    <option value="USA">USA</option>
                  </select>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <TextInput v-model="dropoff.location.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.dropoff.location.postalCode"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <!-- TODO: Replace with a date picker -->
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <!-- TODO: Replace with time picker -->
                  <label>Time</label>
                  <input type="text" class="form-control" placeholder="12:00PM">
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <TextInput v-model="dropoff.contact.name" placeHolder="Name" errorMessage="Please enter a contact name" :validator="$v.dropoff.contact.name"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <EmailInput v-model="dropoff.contact.email" placeHolder="Email" errorMessage="Please enter a valid email" :validator="$v.dropoff.contact.email"/>
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
              <div class="row">
                <div class="col-12 form-group">
                  <label>Starting Bid</label>
                  <input type="text" class="form-control" placeholder="1500">
                </div>
              </div>
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

import { required, helpers, email } from 'vuelidate/lib/validators'
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

export default {
  name: 'shipperCreatePost',
  components: {
    Back,
    WideFormCard,
    TextInput,
    EmailInput,
    ProvinceInput,
  },
  data() {
    return {
      date: new Date(),
      vehicle: {
        year: 2019,
        make: '',
        model: '',
        VIN: '',
        condition: 'New',
      },
      pickup: {
        location: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        date: '',
        time: '',
        contact: {
          email: '',
          phone: '',
        },
      },
      dropoff: {
        location: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
          postalCode: '',
        },
        date: '',
        time: '',
        contact: {
          email: '',
          phone: '',
        },
      },
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
    pickup: {
      location: {
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
      contact: {
        name: {
          required
        },
        email: {
          required,
          email
        }
      },
    },
    dropoff: {
      location: {
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
      contact: {
        name: {
          required
        },
        email: {
          required,
          email
        }
      },
    },
    startingBid: {
      required,
      bidRegex
    }
  },
  methods: {
    years() {
      let items = [];
      for(let year = 1908; year <= this.date.getFullYear(); year++) {
        items.push(year);
      }
      return items;
    },
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
