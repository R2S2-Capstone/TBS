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
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.profile.company.name"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.address.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.profile.company.address.addressLine"/>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.address.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.profile.company.address.city"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
            <ProvinceInput v-model="profile.company.address.province" />
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <CountryInput v-model="profile.company.address.country" />
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="profile.company.address.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.profile.company.address.postalCode"/>
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
import ProvinceInput from '@/components/Form/Input/ProvinceInput.vue'
import CountryInput from '@/components/Form/Input/CountryInput.vue'

import { required, email, helpers } from 'vuelidate/lib/validators'
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)

export default {
  name: 'register',
  components: {
    WideFormCard,
    EmailInput,
    TextInput,
    ProvinceInput,
    CountryInput,
  },
  data() {
    return {
      success: null,
      error: null,
      profile: {
      name: '',
      email: '',
      company: {
        name: '',
        address: {
          addressLine: '',
          city: '',
          province: 'Ontario',
          country: 'Canada',
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
      address: {
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
      this.$store.dispatch('profiles/updateProfile', this.profile)
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
  },
  created() {
    this.fetchProfile()
  }
}
</script>