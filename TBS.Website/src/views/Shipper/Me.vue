<template>
  <div class="container pt-5 pb-5">
    <WideFormCard title="Update Information" :submit="update">
      <div slot="card-content" class="text-center">
        <h5>Your Information</h5>
        <TextInput v-model="profile.name" placeHolder="Name" errorMessage="Please enter your name" :validator="$v.profile.name"/>
        <div>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <TextInput v-model="profile.dealerNumber" placeHolder="Dealer Number" errorMessage="Please enter a dealer number" :validator="$v.profile.dealerNumber"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <TextInput v-model="profile.rin" placeHolder="RIN #" errorMessage="Please enter a RIN number" :validator="$v.profile.rin"/>
            </div>
          </div>
        </div>
        <h5>Company Information</h5>
        <div class="row">
          <div class="col-12">
            <TextInput v-model="profile.company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.profile.company.name"/>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <div class="form-label-group">
              <label>Address</label>
              <input id="Address" v-model="profile.company.address.addressLine"  :class="{ 'is-invalid': validCompanyAddress == false }" type="text" class="form-control" placeholder="Company Address" >
              <p v-if="validCompanyAddress == false" class="text-danger text-center">Please enter a valid company address</p>
            </div>
          </div>
        </div>
        <h5>Contact Information</h5>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.contact.name" placeHolder="Name" errorMessage="Please enter a name" :validator="$v.profile.company.contact.name"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.contact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.profile.company.contact.phoneNumber"/>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <EmailInput v-model="profile.company.contact.email" :validator="$v.profile.company.contact.email"/>
          </div>
        </div>
        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Update</button>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import Swal from 'sweetalert2'

import WideFormCard from '@/components/Form/Card/WideFormCard.vue'
import EmailInput from '@/components/Form/Input/EmailInput.vue'
import TextInput from '@/components/Form/Input/TextInput.vue'

import { required, email, helpers } from 'vuelidate/lib/validators'
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)

export default {
  name: 'register',
  components: {
    WideFormCard,
    EmailInput,
    TextInput,
  },
  data() {
    return {
      success: null,
      error: null,
      validCompanyAddress: null,
      profile: {
        name: '',
        email: '',
        company: {
          name: '',
          address: {
            addressLine: '',
            city: '',
            province: '',
            country: '',
            postalCode: '',
          },
          contact: {
            name: '',
            phoneNumber: '',
            email: '',
          }
        },
        dealerNumber: '',
        rin: '',
      }
    }
  },
  validations: {
    profile:{
      name: {
        required
      },
      company: {
        name: {
          required
        },
        contact: {
          name: {
            required
          },
          phoneNumber: {
            required,
            phoneNumberRegex
          },
          email: {
            required,
            email,
          }
        }
      },
      dealerNumber: {
        required
      },
      rin: {
        required
      },
    },
  },
  methods: {
    update() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        return;
      }
      let tempProfile = this.profile
      delete tempProfile.reviews
      delete tempProfile.posts
      this.$store.dispatch('profiles/updateProfile', tempProfile)
        .then(() => {
          this.error = false
          this.success = true
          Swal.fire({
            type: 'success',
            title: 'Success',
            text: `Profile has successfully been updated!`,
          })
        })
        .catch(() => {
          this.error = true
          this.success = false
        })
    },
    fetchProfile() {
      this.$store.dispatch('profiles/getMyProfile')
        .then((response) => {
          this.profile = response.data.result
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
    parseAddress(address) {
      try {
        this.profile.company.address.addressLine = `${address.find(a => a.types.includes("street_number")).short_name} ${address.find(a => a.types.includes("route")).long_name}`
        this.profile.company.address.city = address.find(a => a.types.includes("locality")).long_name
        this.profile.company.address.province = address.find(a => a.types.includes("administrative_area_level_1")).short_name
        this.profile.company.address.country = address.find(a => a.types.includes("country")).long_name
        this.profile.company.address.postalCode = address.find(a => a.types.includes("postal_code")).long_name
        this.validCompanyAddress = true
      } catch {
        this.validCompanyAddress = false
      }
    }
  },
  mounted() {
    // eslint-disable-next-line
    var address = new google.maps.places.Autocomplete(document.getElementById('Address'))
    // eslint-disable-next-line
    google.maps.event.addListener(address, 'place_changed', () => {
      if (address.getPlace().address_components == null) {
        this.validCompanyAddress = false
        return
      }
      this.parseAddress(address.getPlace().address_components)
    })
  },
  created() {
    this.fetchProfile()
  }
}
</script>

<style lang="scss" scoped>
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>