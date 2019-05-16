<template>
  <div class="container pt-5">
    <FormNarrowCard title="Register" :submit="submit">
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
          <FormEmail v-model="email" :validator="$v.email"/>
          <FormPassword v-model="password" :validator="$v.password"/>
          <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>
          <!-- TODO: More data -->
        </div>

        <div v-else>
          <FormEmail v-model="email" :validator="$v.email"/>
          <FormPassword v-model="password" :validator="$v.password"/>
          <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>
          <!-- TODO: More data -->
        </div>

        <div class="mb-3">
          <router-link :to="{ name: 'login' }">Already have an account? Login here</router-link>
        </div>

        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Register</button>
      </div>
    </FormNarrowCard>
  </div>
</template>

<script>
import FormNarrowCard from '@/components/Form/Card/FormNarrowCard.vue'
import FormEmail from '@/components/Form/Input/FormEmail.vue'
import FormPassword from '@/components/Form/Input/FormPassword.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
  name: 'register',
  components: {
    FormNarrowCard,
    FormEmail,
    FormPassword
  },
  data() {
    return {
      email: '',
      password: '',
      confirmationPassword: '',
      isShipper: true,
      success: null,
      error: null,
      errorMessage: 'An error has occured, make sure your passwords match and your email is unique'
    }
  },
  validations: {
    email: {
      required,
      email
    },
    password: {
      required,
      minLength: minLength(6),
      passwordRegex
    },
    confirmationPassword: {
      required,
      sameAsPassword: sameAs('password')
    }
  },
  methods: {
    submit() {
        this.$v.$touch()
        // TODO: Will no longer work once more validations are specifically added for each type of account
        if (this.$v.$invalid) {
            return
        }
        this.$store.dispatch('authentication/register', { email: this.email, password: this.password })
          .then(() => {
            this.success = true
          })
          .catch(() => {
            this.error = true
          })
    },
  }
}
</script>