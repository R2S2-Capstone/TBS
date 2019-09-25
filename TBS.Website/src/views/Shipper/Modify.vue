<template>
  <div class="container pt-5 pb-5">
    <Back/>
    <WideFormCard :title="type + ' Post'">
      <div slot="card-information" class="text-center">
        <h4 v-if="error" class="text-danger pb-4">Failed to {{ type.toLowerCase()}} post</h4>
        <h4 v-if="deleteError" class="text-danger pb-4">Failed to delete post</h4>
        <h4 v-if="failedToLoadError" class="text-danger pb-4">Failed to load post</h4>
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
            </div>
          </div>
          <div class="row pt-3">
            <div class="col-12">
              <h5>Pickup</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="form-label-group">
                <input id="PickupAddress" v-model="post.pickupLocation.addressLine" :class="{ 'is-invalid': validPickupAddress == false }" type="text" class="form-control" placeholder="Pickup Address" />
                <p v-if="validPickupAddress == false" class="text-danger text-center">Please enter a valid pickup address</p>
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
              <h5>Dropoff</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-12">
                  <div class="form-label-group">
                    <input id="DropoffAddress" v-model="post.dropoffLocation.addressLine"  :class="{ 'is-invalid': validDropoffAddress == false }" type="text" class="form-control" placeholder="Dropoff Address" />
                    <p v-if="validDropoffAddress == false" class="text-danger text-center">Please enter a valid dropoff address</p>
                  </div>
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
        <div class="col-12">
          <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" @click="submit" type="submit">{{ type }}</button>
        </div>
        <div class="col-12 pt-2" v-if="type == 'Update'">
          <button type="button" class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white" @click="confirmDelete">Delete</button>
        </div>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import Back from '@/components/Back.vue'
import WideFormCard from '@/components/Form/Card/WideFormCard.vue'

import TextInput from '@/components/Form/Input/TextInput.vue'
import EmailInput from '@/components/Form/Input/EmailInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import ConditionInput from '@/components/Form/Input/ConditionInput.vue'
import YearInput from '@/components/Form/Input/YearInput.vue'

import { required, helpers, email } from 'vuelidate/lib/validators'
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

import postUtilities from '@/utils/postUtilities.js'

export default {
  name: 'shipperCreatePost',
  components: {
    Back,
    WideFormCard,
    TextInput,
    EmailInput,
    DateInput,
    ConditionInput,
    YearInput,
  },
  data() {
    return {
      error: false,
      deleteError: false,
      failedToLoadError: false,
      validPickupAddress: null,
      validDropoffAddress: null,
      post: {
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
        pickupContact: {
          name: '',
          email: '',
          phoneNumber: '',
        },
        dropoffLocation: {
          addressLine: '',
          city: '',
          province: '',
          country: '',
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
          required
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
        bidRegex,
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
          if (this.type.toLowerCase() == 'create') {
            Swal.fire({
              type: 'success',
              title: 'Success',
              text: `Post has successfully been created!`,
            })
          } else {
            Swal.fire({
              type: 'success',
              title: 'Success',
              text: `Post has successfully been updated!`,
            })
          }
          this.$router.push({ name: 'shipperHome' })
				})
				.catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to process your request. Please try again!',
          })
					this.error = true
				}) 
    },
    confirmDelete() {
      Swal.fire({
        title: 'Are you sure?',
        text: `Are you sure you want to delete this post?`,
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete my post!'
      }).then((result) => {
        if (result.value) {
          this.deletePost()
        }
      })
    },
    deletePost() {
      this.$store.dispatch('posts/deletePost',  { id: this.post.id })
        .then(() => {
          Swal.fire({
            type: 'success',
            title: 'Deleted',
            text: 'Post has successfully been deleted!',
          })
          this.$router.push({name: 'shipperHome' })
        })
        .catch(() => {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong! We are unable to delete this post. Please try again!',
          })
          this.deleteError = true
        })
    },
    parseDate(date) {
      return postUtilities.parseDate(date)
    },
    parsePickupLocationAddress(address) {
      try {
        this.post.pickupLocation.addressLine = `${address[0].long_name} ${address[1].long_name}`
        this.post.pickupLocation.city = address[2].long_name
        this.post.pickupLocation.province = address[4].short_name
        this.post.pickupLocation.country = address[5].long_name
        this.post.pickupLocation.postalCode = address[6].long_name
        this.validPickupAddress = true
      } catch {
        this.validPickupAddress = false
      }
    },
    parseDropoffLocationAddress(address) {
      try {
        this.post.dropoffLocation.addressLine = `${address[0].long_name} ${address[1].long_name}`
        this.post.dropoffLocation.city = address[2].long_name
        this.post.dropoffLocation.province = address[4].short_name
        this.post.dropoffLocation.country = address[5].long_name
        this.post.dropoffLocation.postalCode = address[6].long_name
        this.validDropoffAddress = true
      } catch {
        this.validDropoffAddress = false
      }
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
  mounted() {
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
  },
  created() {
      if (this.type == 'Update') {
        this.$store.dispatch('posts/getPostById', { postId: this.$route.params.id })
          .then((response) => {
            this.post = response.data.result
            this.post.vehicle.year = this.post.vehicle.year.toString() 
            this.post.vehicle.condition = utilities.parseVehicleCondition(this.post.vehicle.condition)
            this.post.startingBid = this.post.startingBid.toString()
            this.post.pickupDate = this.post.pickupDate.split('T')[0]
            this.post.dropoffDate = this.post.dropoffDate.split('T')[0]
            this.failedToLoadError = false
          })
          .catch(() => {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Something went wrong! We are unable to load this post. Please try again!',
            })
            this.failedToLoadError = true
          })
      } else {
        this.$store.dispatch('profiles/getMyProfile')
          .then((response) => {
            this.post.dropoffContact = response.data.result.company.contact
            this.post.dropoffLocation = response.data.result.company.address
            this.failedToLoadError = false
          })
          .catch(() => {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Something went wrong! We are unable to load this post. Please try again!',
            })
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
