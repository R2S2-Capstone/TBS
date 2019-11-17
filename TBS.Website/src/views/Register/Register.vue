<template>
  <div class="container pt-5 pb-5">
    <WideFormCard title="Register" :submit="submit">
      <div slot="card-information">
        
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
          <div class="col-12">
            <TextInput v-model="company.name" placeHolder="Name" errorMessage="Please enter a company name" :validator="$v.company.name"/>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <div class="form-label-group">
              <label>Address</label>
              <input id="CompanyAddress" v-model="company.address.addressLine"  :class="{ 'is-invalid': validCompanyAddress == false }" type="text" class="form-control" placeholder="Company address" >
              <p v-if="validCompanyAddress == false" class="text-danger text-center">Please enter a valid company address</p>
            </div>
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

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)
const phoneNumberRegex = helpers.regex('phoneNumberRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)

export default {
  name: 'register',
  components: {
    WideFormCard,
    EmailInput,
    PasswordInput,
    TextInput,
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
      validCompanyAddress: null,
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
            this.$router.push({ name: 'registerConfirm' })
          })
          .catch(() => {
            this.error = true
            
          })
    },
    parseAddress(address) {
      try {
        this.company.address.addressLine = `${address.find(a => a.types.includes("street_number")).short_name} ${address.find(a => a.types.includes("route")).long_name}`
        this.company.address.city = address.find(a => a.types.includes("locality")).long_name
        this.company.address.province = address.find(a => a.types.includes("administrative_area_level_1")).short_name
        this.company.address.country = address.find(a => a.types.includes("country")).long_name
        this.company.address.postalCode = address.find(a => a.types.includes("postal_code")).long_name
        this.validCompanyAddress = true
      } catch {
        this.validCompanyAddress = false
      }
    }
  },
  mounted() {
    // eslint-disable-next-line
    var address = new google.maps.places.Autocomplete(document.getElementById('CompanyAddress'))
    // eslint-disable-next-line
    google.maps.event.addListener(address, 'place_changed', () => {
      if (address.getPlace().address_components == null) {
        this.validCompanyAddress = false
        return
      }
      this.parseAddress(address.getPlace().address_components)
    })
  }
}
</script>

<style lang="scss" scoped>
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>