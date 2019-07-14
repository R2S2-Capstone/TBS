<template>
  <div class="container pt-5">
    <NarrowFormCard title="Reset Password" :submit="submit">
      <div slot="card-information">
        <p v-if="emailSent" class="text-success text-center mb-3">Password reset email sent</p>
        <p v-if="error" class="text-danger text-center mb-3">An error has occured, please try again</p>
      </div>
      <div slot="card-content">
        <EmailInput
          v-model="email"
          :validator="$v.email"
        />
        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Reset Password</button>
      </div>
    </NarrowFormCard>
  </div>
</template>

<script>
import NarrowFormCard from '@/components/Form/Card/NarrowFormCard.vue'
import EmailInput from '@/components/Form/Input/EmailInput.vue'

import { required, email } from 'vuelidate/lib/validators'

export default {
  name: 'resetPassword',
  components: {
    NarrowFormCard,
    EmailInput,
  },
  data() {
    return {
      email: '',
      error: false,
      emailSent: false,
    }
  },
  validations: {
    email: {
      required,
      email
    },
  },
  methods: {
    submit() {
      this.$v.$touch()
      if (this.$v.$error) {
        return
      }
      this.$store.dispatch('authentication/resetPassword', { email: this.email })
        .then(() => {
          this.error = false
          this.emailSent = true
        })
        .catch(() => {
          this.error = true
          this.emailSent = false
        })
    }
  }
}
</script>