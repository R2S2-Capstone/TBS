<template>
  <div class="card text-center pointer mb-5" @click="showDetailedPost()">
    <h4 class="title pt-3">{{ `${post.vehicle.year} ${post.vehicle.make} ${post.vehicle.model}` }}</h4>
    <hr>
    <p>{{ post.pickupLocation.city }} <i class="fas fa-arrow-right"></i> {{ post.dropoffLocation.city }}</p>
    <p>{{ trimDate(post.pickupDate) }} <i class="fas fa-arrow-right"></i> {{ trimDate(post.dropoffDate) }}</p>
    <p>Starting Bid: ${{ post.startingBid }}</p>
    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white ml-5 mr-5 mb-3">Details</button>
  </div>
</template>

<script>
export default {
  name: 'shipperPost',
  props: {
    post: Object
  },
  methods: {
    showDetailedPost() {
      this.$router.push({ name: 'viewDetailedShipperPost', params: { id: this.post.id.toString() } })
    },
    trimDate(date) {
      return new Date(date).toISOString().split('T')[0]
    }
  }
}
</script>
