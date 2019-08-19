<template>
  <div class="container pt-5">
    <Back/>
    <WideFormCard :title="type + ' Availability Post'">
      <div slot="card-information" class="text-center">
        <h4 v-if="error" class="text-danger pb-4">Failed to {{ type.toLowerCase()}} post</h4>
        <h4 v-if="deleteError" class="text-danger pb-4">Failed to delete post</h4>
        <h4 v-if="failedToLoadError" class="text-danger pb-4">Failed to load post</h4>
      </div>
      <div slot="card-content" class="text-center">
        <form @submit.prevent="submit">
          <div class="row">
            <div class="col-12">
              <h5>Pickup Information</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-12">
                  <TextInput v-model="post.pickupLocation" placeHolder="Pickup city" errorMessage="Please enter a pickup city" :validator="$v.post.pickupLocation"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Date</label>
                  <DateInput v-model="post.pickupDateValue" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Time</label>
                  <TimeInput v-model="post.pickupTime" />
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-5">
            <div class="col-12">
              <h5>Delivery Information</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-12">
                  <TextInput v-model="post.dropoffLocation" placeHolder="Dropoff City" errorMessage="Please enter a dropoff city" :validator="$v.post.dropoffLocation"/>
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Date</label>
                  <DateInput v-model="post.dropoffDateValue" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                  <label>Time</label>
                  <TimeInput v-model="post.dropoffTime" />
                </div>
              </div>
            </div>
            <div class="row pt-5">
              <div class="col-12">
                <h5>Other Details</h5>
                <hr>
              </div>
              <div class="col-12">
                <TextInput v-model="post.startingBid" placeHolder="Starting Bid" errorMessage="Please enter a starting bid" :validator="this.$v.post.startingBid" />
              </div>
              <div class="col-12">
                <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Trailer Type</label>
                    <TrailerInput v-model="post.trailerType" />
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-12">
                    <label>Spaces Available</label>
                    <CapacityInput v-model="post.spacesAvailable" />
                  </div>
                </div>
              </div>
              <div class="col-12">
                <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" type="submit">{{ type }}</button>
              </div>
              <div class="col-12 pt-2" v-if="type == 'Update'">
                <button type="button" class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" @click="showModal = true">Delete</button>
                <!-- <Modal v-if="showModal" title="Delete post confirmation" description="Are you sure you want to delete this post?" submitText="Yes" :submit="deletePost" :cancel="() => { showModal = false}" /> -->
              </div>
            </div>
          </div>
        </form>
      </div>
    </WideFormCard>
  </div>
</template>

<script>
import Back from '@/components/Back.vue'
import WideFormCard from '@/components/Form/Card/WideFormCard.vue'

import TextInput from '@/components/Form/Input/TextInput.vue'
import DateInput from '@/components/Form/Input/DateInput.vue'
import TimeInput from '@/components/Form/Input/TimeInput.vue'
import CapacityInput from '@/components/Form/Input/CapacityInput.vue'
import TrailerInput from '@/components/Form/Input/TrailerInput.vue'


import { required, helpers } from 'vuelidate/lib/validators'
const bidRegex = helpers.regex('bidRegex', /^[+]?([0-9]+(?:[.][0-9]*)?|\.[0-9]+)$/)

import utilities from '@/utils/postUtilities.js'

export default {
  name: 'carrierCreatePost',
  components: {
    Back,
    WideFormCard,
    TextInput,
    DateInput,
    TimeInput,
    CapacityInput,
    TrailerInput,
  },
  data() {
    return {
      error: false,
      deleteError: false,
      failedToLoadError: false,
      post: {
        pickupLocation: '',
        pickupDate: '', 
        pickupDateValue: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0],
        pickupTime: new Date().getHours() + ":" + new Date().getMinutes(),
        dropoffLocation: '',
        dropoffDate: '',
        dropoffDateValue: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0],
        dropoffTime: new Date().getHours() + ":" + new Date().getMinutes(),
        trailerType: 'Car Carrier',
        spacesAvailable: 1,
        startingBid: '',
      }
    }
  },
  validations: {
    post: {
      pickupLocation: {
        required,
      },
      dropoffLocation: {
        required
      },
      startingBid: {
        required,
        bidRegex,
      },
    }
  },
  methods: {
    submit() {
      this.$v.$touch()
      if (this.$v.$invalid) {
				return
      }
      this.post.pickupDate = `${this.post.pickupDateValue} ${this.post.pickupTime}`
      this.post.dropoffDate = `${this.post.dropoffDateValue} ${this.post.dropoffTime}`
      this.post.trailerType = this.post.trailerType.replace(' ', '')
      // Required as create post cannot have a postId
      if (this.type == 'Update') {
        this.$store.dispatch('posts/updatePost', { id: this.post.id, pickupLocation: this.post.pickupLocation, pickupDate: this.post.pickupDate, dropoffLocation: this.post.dropoffLocation, dropoffDate: this.post.dropoffDate, spacesAvailable: this.post.spacesAvailable, startingBid: this.post.startingBid })
        this.$store.dispatch('posts/updatePost', this.post)
        .then(() => {
          this.$router.push({name: 'carrierHome' })
        })
        .catch(() => {
          this.error = true
        })
      } else {
        this.$store.dispatch('posts/createPost', { pickupLocation: this.post.pickupLocation, pickupDate: this.post.pickupDate, dropoffLocation: this.post.dropoffLocation, dropoffDate: this.post.dropoffDate, spacesAvailable: this.post.spacesAvailable, startingBid: this.post.startingBid })
        .then(() => {
          this.$router.push({ name: 'carrierHome' })
        })
        .catch(() => {
          this.error = true
        })
      }
    },
    deletePost() {
      this.$store.dispatch('posts/deletePost', { id: this.post.id })
        .then(() => {
          this.$router.push({ name: 'carrierHome' })
        })
        .catch(() => {
          this.deleteError = true
        })
    }
  },
  computed: {
    type() {
      if (this.$route.params.id != null) {
        return 'Update'
      } else {
        return 'Create'
      }
    },
  },
  created() {
    if (this.type == 'Update') {
      this.$store.dispatch('posts/getPostById', { postId: this.$route.params.id })
				.then((response) => {
          this.post = response.data.result
          this.post.startingBid = this.post.startingBid.toString()
          this.post.trailerType = utilities.parseTrailerType(this.post.trailerType)
          this.post.pickupDateValue = this.post.pickupDate.split('T')[0]
          this.post.pickupTime = this.post.pickupDate.split('T')[1].substring(0, 5)
          this.post.dropoffDateValue = this.post.dropoffDate.split('T')[0]
          this.post.dropoffTime = this.post.dropoffDate.split('T')[1].substring(0, 5)
          this.failedToLoadError = false
				})
				.catch(() => {
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