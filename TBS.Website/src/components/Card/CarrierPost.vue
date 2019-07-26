<template>
  <div class="card text-center pointer mb-5" @click="showDetailedPost()">
    <h4 class="title pt-3">{{ `Spaces Available: ${post.spacesAvailable}` }}</h4>
    <hr>
    <p>{{ post.pickupLocation }} <i class="fas fa-arrow-right"></i> {{ post.dropoffLocation }}</p>
    <p>{{ trimDate(post.pickupDate) }} <i class="fas fa-arrow-right"></i> {{ trimDate(post.dropoffDate) }}</p>
    <p>Starting Bid: ${{ post.startingBid }}</p>
    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white ml-5 mr-5 mb-3">Details</button>
  </div>
</template>

<script>
export default {
  name: 'carrierPost',
  props: {
    post: Object
  },
  methods: {
    showDetailedPost() {
      console.log('Showing detailed post of ' + this.post.id)
    },
    trimDate(date) {
      return new Date(date).toISOString().split('T')[0]
    }
  }
}
</script>