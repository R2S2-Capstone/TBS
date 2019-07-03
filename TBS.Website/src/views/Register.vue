<template>
  <div class="container pt-5">
    <FormWideCard title="Register" :submit="submit">
      <div slot="card-information">
        <p v-if="success" class="text-success text-center mb-3">A confirmation email has been sent. Emails may take up to five minutes to send</p>
        <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
      </div>

      <div slot="card-content" class="text-center">
        <ul class="nav nav-tabs nav-justified mb-2" role="tablist">
          <li class="nav-item">
            <a class="nav-link active" href="" data-toggle="tab" role="tab" aria-controls="shipper"
              aria-selected="true" @click="isShipper = true">Shipper</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="" data-toggle="tab" role="tab" aria-controls="carrier"
              aria-selected="false" @click="isShipper = false">Carrier</a>
          </li>
        </ul>
        <div v-if="isShipper">
          <h5>Your Information</h5>
          <FormEmail v-model="email" :validator="$v.email"/>
          <FormText v-model="name" placeHolder="Name" errorMessage="Please enter your name" :validator="$v.name"/>
          <FormPassword v-model="password" :validator="$v.password"/>
          <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>
          <h5>Company Information</h5>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.company.name"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.addressLine" placeHolder="Address Line" errorMessage="Please enter an address" :validator="$v.company.address.addressLine"/>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.company.address.city"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.country" placeHolder="Country" errorMessage="Please enter a country" :validator="$v.company.address.country"/>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.provinceCode" placeHolder="Province" errorMessage="Please enter a province" :validator="$v.company.address.provinceCode"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.postalCode" placeHolder="Postal code" errorMessage="Please enter a valid postal code" :validator="$v.company.address.postalCode"/>
            </div>
          </div>
          <h5>Contact Information</h5>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.contact.name" placeHolder="Name" errorMessage="Please enter a name" :validator="$v.company.contact.name"/>

            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.contact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.company.contact.phoneNumber"/>
            </div>
          </div>
        </div>

        <div v-else>
          <h5>Your Information</h5>
          <FormEmail v-model="email" :validator="$v.email"/>
          <FormText v-model="name" placeHolder="Name" errorMessage="Please enter your name" :validator="$v.name"/>
          <FormPassword v-model="password" :validator="$v.password"/>
          <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="dealerNumber" placeHolder="Dealer Number" errorMessage="Please enter a company address" :validator="$v.dealerNumber"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="rin" placeHolder="RIN # (Drivers license)" errorMessage="Please enter a company address" :validator="$v.rin"/>
            </div>
          </div>
          <h5>Company Information</h5>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.company.name"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.addressLine" placeHolder="Address Line" errorMessage="Please enter a address" :validator="$v.company.address.addressLine"/>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.city" placeHolder="City" errorMessage="Please enter a city" :validator="$v.company.address.city"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.country" placeHolder="Country" errorMessage="Please enter a country" :validator="$v.company.address.country"/>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.provinceCode" placeHolder="Province" errorMessage="Please enter a province" :validator="$v.company.address.provinceCode"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.address.postalCode" placeHolder="Postal code" errorMessage="Please enter a valid postal code" :validator="$v.company.address.postalCode"/>
            </div>
          </div>
          <h5>Contact Information</h5>
          <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.contact.name" placeHolder="Name" errorMessage="Please enter a name" :validator="$v.company.contact.name"/>

            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
              <FormText v-model="company.contact.phoneNumber" placeHolder="Phone Number" errorMessage="Please enter a valid phone number" :validator="$v.company.contact.phoneNumber"/>
            </div>
          </div>
        </div>

        <div class="mb-3">
          <router-link :to="{ name: 'login' }">Already have an account? Login here</router-link>
        </div>

        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Register</button>
      </div>
    </FormWideCard>
  </div>
</template>

<script>
import FormWideCard from '@/components/Form/Card/FormWideCard.vue'
import FormEmail from '@/components/Form/Input/FormEmail.vue'
import FormPassword from '@/components/Form/Input/FormPassword.vue'
import FormText from '@/components/Form/Input/FormText.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
  name: 'register',
  components: {
    FormWideCard,
    FormEmail,
    FormPassword,
    FormText
  },
  data() {
    return {
      name: '',
      email: '',
      password: '',
      confirmationPassword: '',
      isShipper: true,
      success: null,
      error: null,
      errorMessage: 'An error has occured, make sure your passwords match and your email is unique',
      company: {
        name: '',
        address: {
          addressLine: '',
          city: '',
          provinceCode: '',
          country: '',
          postalCode: '',
        },
        contact: {
          name: '',
          phoneNumber: ''
        }
      },
      dealerNumber: '',
      rin: ''
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
        provinceCode: {
          required,
          // provinceCodeRegex
        },
        country: {
          required
        },
        postalCode: {
          required,
          // postalCodeRegex
        },
      },
      contact: {
        name: {
          required
        },
        phoneNumber: {
          required,
          // phoneNumberRegex
        }
      }
    },
    dealerNumber: {
      required
    },
    rin: {
      required
    }
  },
  methods: {
    submit() {
        this.$v.$touch()
        if (this.$v.$invalid && (!this.isShipper && (this.$v.$dealerNumber.$error || this.$v.$rin.$error))) {
          return
        }
        this.$store.dispatch('authentication/register', { email: this.email, password: this.password, accountType: this.isShipper ? 'Shipper' : 'Carrier' })
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