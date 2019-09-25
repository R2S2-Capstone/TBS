<template>
  <div class="card text-center mb-5">
    <h4 class="title pt-3">{{ `${post.vehicle.year} ${post.vehicle.make} ${post.vehicle.model}` }}</h4>
    <table class="table">
      <tr>
        <th scope="col"></th>
        <th scope="col">City</th>
        <th scope="col">Date</th>
      </tr>
      <tr>
        <th scope="row">Pickup In: </th>
        <td>{{ formatAddress(post.pickupLocation) }}</td>
        <td>{{ parseDate(post.pickupDate) }}</td>
      </tr>
      <tr>
        <th scope="row">Deliver To: </th>
        <td>{{ formatAddress(post.dropoffLocation) }}</td>
        <td>{{ parseDate(post.dropoffDate) }}</td>
      </tr>
    </table>
    <p>Starting Bid: {{ formatMoney(post.startingBid) }}</p>
    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white ml-5 mr-5 mb-3" @click="showDetailedPost()">Details</button>
  </div>
</template>

<script>
import postUtilities from '@/utils/postUtilities.js'

export default {
  name: 'shipperPost',
  props: {
    post: Object
  },
  methods: {
    showDetailedPost() {
      this.$router.push({ name: 'viewDetailedShipperPost', params: { id: this.post.id.toString() } })
    },
    parseDate(date) {
      return postUtilities.parseDate(date)
    },
    formatAddress(address) {
      return postUtilities.formatAddress(address)
    },
    formatMoney(amount) {
      return postUtilities.formatMoney(amount)
    }
  }
}
</script>
