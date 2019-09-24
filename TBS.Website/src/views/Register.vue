<template>
  <div class="container pt-5 pb-5">
    <WideFormCard title="Register" :submit="submit">
      <div slot="card-information">
        <p v-if="success" class="text-success text-center mb-3">A confirmation email has been sent. Emails may take up to five minutes to send</p>
        <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
      </div>

      <div slot="card-content" class="text-center">
        <ul class="nav nav-tabs nav-justified mb-2" role="tablist">
          <li class="nav-item">
            <a class="nav-link active" href="" data-toggle="tab" role="tab" aria-controls="carrier"
              aria-selected="true" @click="isShipper = false">Carrier</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="" data-toggle="tab" role="tab" aria-controls="shipper"
              aria-selected="false" @click="isShipper = true">Shipper</a>
          </li>
        </ul>

        <h5>Your Information</h5>
        <EmailInput v-model="email" :validator="$v.email"/>
        <TextInput v-model="name" placeHolder="Name" errorMessage="Please enter your name" :validator="$v.name"/>
        <PasswordInput v-model="password" :validator="$v.password"/>
        <PasswordInput v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>
        <div v-if="isShipper">
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <TextInput v-model="dealerNumber" placeHolder="Dealer Number" errorMessage="Please enter a dealer number" :validator="$v.dealerNumber"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <TextInput v-model="rin" placeHolder="RIN #" errorMessage="Please enter a RIN number" :validator="$v.rin"/>
            </div>
          </div>
        </div>
        <div v-else>
          <TextInput v-model="driversLicense" placeHolder="Drivers License" errorMessage="Please enter a drivers license number" :validator="$v.driversLicense"/>
        </div>
        <h5>Company Information</h5>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.company.name"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.address.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.company.address.addressLine"/>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.address.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.company.address.city"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12 form-label-group">
            <ProvinceInput v-model="company.address.province" />
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <CountryInput v-model="company.address.country" />
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.address.postalCode" placeHolder="Postal/Zip code" errorMessage="Please enter a valid postal/zip code" :validator="$v.company.address.postalCode"/>
          </div>
        </div>
        <h5>Contact Information</h5>
        <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.contact.name" placeHolder="Name" errorMessage="Please enter a name" :validator="$v.company.contact.name"/>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12">
            <TextInput v-model="company.contact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.company.contact.phoneNumber"/>
          </div>
        </div>

        <div class="row">
          <div class="col-12">
            <EmailInput v-model="company.contact.email" :validator="$v.company.contact.email"/>
          </div>
        </div>

        <div class="mb-3">
          <router-link :to="{ name: 'login' }">Already have an account? Login here</router-link>
        </div>

        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Register</button>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import WideFormCard from '@/components/Form/Card/WideFormCard.vue'
import EmailInput from '@/components/Form/Input/EmailInput.vue'
import PasswordInput from '@/components/Form/Input/PasswordInput.vue'
import TextInput from '@/components/Form/Input/TextInput.vue'
import ProvinceInput from '@/components/Form/Input/ProvinceInput.vue'
import CountryInput from '@/components/Form/Input/CountryInput.vue'

import { required, minLength, maxLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)

export default {
  name: 'register',
  components: {
    WideFormCard,
    EmailInput,
    PasswordInput,
    TextInput,
    ProvinceInput,
    CountryInput,
  },
  data() {
    return {
      name: '',
      email: '',
      password: '',
      confirmationPassword: '',
      isShipper: false,
      success: null,
      error: null,
      errorMessage: 'An error has occured, make sure your passwords match and your email is unique',
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
      driversLicense: '',
    }
  },
  validations: {
    email: {
      required,
      email
    },
    name: {
      required
    },
    password: {
      required,
      minLength: minLength(6),
      passwordRegex
    },
    confirmationPassword: {
      required,
      sameAsPassword: sameAs('password')
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
    driversLicense: {
      required
    }
  },
  methods: {
    submit() {
        this.$v.$touch()
        if (this.$v.$invalid && this.isShipper && (this.$v.dealerNumber.$error || this.$v.rin.$error)) return
        if (this.$v.$invalid && !this.isShipper && this.$v.driversLicense.$error) return
        this.$store.dispatch('authentication/register', { email: this.email, password: this.password, name: this.name, company: this.company, accountType: this.isShipper ? 'Shipper' : 'Carrier', dealerNumber: this.dealerNumber, rin: this.rin, driversLicense: this.driversLicense })
          .then(() => {
            this.error = false
            this.success = true
          })
          .catch(() => {
            this.error = true
            this.success = false
          })
    },
  }
}
</script>