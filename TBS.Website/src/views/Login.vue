<template>
  <div class="container pt-5">
    <FormNarrowCard title="Login" :submit="submit" v-if="this.$route.name === 'login'">
      <div slot="card-information">
				<p v-if="redirect" class="text-danger text-center mb-3">You must be logged in to view this. Please login below.</p>
        <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
      </div>

      <div slot="card-content" class="text-center">
        <FormEmail v-model="email" :validator="$v.email"/>
        <FormPassword v-model="password" :validator="$v.password"/>

        <div class="mb-3">
          <router-link :to="{ name: 'register' }">Don't have an account? Register here</router-link>
        </div>

        <div class="mb-3">
          <router-link :to="{ name: 'resetPassword' }">Forgot your password?</router-link>
        </div>

        <button class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase text-white" type="submit">Login</button>
      </div>
    </FormNarrowCard>
  </div>
</template>

<script>
import FormNarrowCard from '@/components/Form/Card/FormNarrowCard.vue'
import FormEmail from '@/components/Form/Input/FormEmail.vue'
import FormPassword from '@/components/Form/Input/FormPassword.vue'

import { required, minLength, email } from 'vuelidate/lib/validators'

export default {
	name: 'login',
	components: {
		FormNarrowCard,
		FormEmail,
		FormPassword,
	},
	data() {
		return {
			email: '',
			password: '',
			redirect: this.$route.params.redirect,
			error: false,
			errorMessage: "Failed to login. Please try again"
		};
	},
	validations: {
		email: {
			required,
			email
		},
		password: {
			required,
			minLength: minLength(6)
		}
	},
	methods: {
		submit() {
			this.$v.$touch();
			if (this.$v.$invalid) {
				return;
			}
			this.$store.dispatch('authentication/login', { email: this.email, password: this.password })
          .then(() => {
						this.$router.push({ name: 'home' })
          })
          .catch(() => {
            this.error = true
          })
		}
	}
}
</script>