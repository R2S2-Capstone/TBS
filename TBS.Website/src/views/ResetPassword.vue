<template>
  <FormNarrowCard title="Reset Password" :submit="submit">
    <div slot="card-information">
      <p v-if="emailSent" class="text-success text-center mb-3">Password reset email sent</p>
      <p v-if="passwordReset" class="text-success text-center mb-3">Password has been reset</p>
      <p v-if="error" class="text-danger text-center mb-3">An error has occured, please try again</p>
    </div>
    <div slot="card-content">
      <div v-if="!token">
        <FormEmail
          v-model="email"
          :validator="$v.email"
        />
      </div>
      <div v-else>
        <FormPassword
          v-model="password"
          :validator="$v.password"
        />
        <FormPassword
          v-model="confirmationPassword"
          confirmationPassword="true"
          :validator="$v.confirmationPassword"
        />
      </div>
      <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase" type="submit">Reset Password</button>
    </div>
  </FormNarrowCard>
</template>

<script>
import FormNarrowCard from '@/components/Form/Card/FormNarrowCard.vue'
import FormEmail from '@/components/Form/Input/FormEmail.vue'
import FormPassword from '@/components/Form/Input/FormPassword.vue'

import { required, minLength, email, sameAs, helpers } from "vuelidate/lib/validators"
const passwordRegex = helpers.regex("passwordRegex", /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
  name: 'resetPassword',
  components: {
    FormNarrowCard,
    FormEmail,
    FormPassword
  },
  data() {
    return {
      email: this.$route.query.email,
      password: '',
      confirmationPassword: '',
      token: this.$route.query.token || '',
      error: false,
      emailSent: false,
      passwordReset: false
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
      sameAsPassword: sameAs("password")
    }
  },
  methods: {
      submit() {
        this.$v.$touch()
        if (this.token) {
          // Token is validated via firebase
          if (this.$v.password.$error || this.$v.confirmationPassword.$error) {
            return
          }
          // TODO: Reset password
        } else {
          if (this.$v.email.$error) {
            return
          }
          // TODO: Send password reset
        }
      }
  }
}
</script>