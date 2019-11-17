<template>
  <div class="card text-center mb-5">
    <h4 class="title pt-3">{{ `Spaces Available: ${post.spacesAvailable}` }}</h4>
    <table class="table">
      <tr>
        <th style="width: 33.3%"></th>
        <th style="width: 33.3%">City</th>
        <th style="width: 33.3%">Date</th>
      </tr>
      <tr>
        <th>Pickup In: </th>
        <td>{{ post.pickupLocation }}</td>
        <td>{{ parseDate(post.pickupDate) }}</td>
      </tr>
      <tr>
        <th>Dropoff To: </th>
        <td>{{ post.dropoffLocation }}</td>
        <td>{{ parseDate(post.dropoffDate) }}</td>
      </tr>
    </table>
    <p>Minimum Bid: {{ formatMoney(post.startingBid) }}</p>
    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white ml-5 mr-5 mb-3" @click="showDetailedPost()">Details</button>
  </div>
</template>

<script>
import postUtilities from '@/utils/postUtilities.js'

export default {
  name: 'carrierPost',
  props: {
    post: Object
  },
  methods: {
    showDetailedPost() {
      this.$router.push({ name: 'viewDetailedCarrierPost', params: { id: this.post.id.toString() } })
    },
    parseDate(date) {
      return postUtilities.parseDate(date)
    },
    formatMoney(amount) {
      return postUtilities.formatMoney(amount)
    }
  }
}
</script>