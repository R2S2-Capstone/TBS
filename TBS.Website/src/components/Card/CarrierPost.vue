<template>
  <div class="card text-center mb-5">
    <h4 class="title pt-3">{{ `Spaces Available: ${post.spacesAvailable}` }}</h4>
    <table class="table">
      <tr>
        <th></th>
        <th>City</th>
        <th>Date</th>
      </tr>
      <tr>
        <th>Pickup In: </th>
        <td>{{ post.pickupLocation }}</td>
        <td>{{ trimDate(post.pickupDate) }}</td>
      </tr>
      <tr>
        <th>Dropoff To: </th>
        <td>{{ post.dropoffLocation }}</td>
        <td>{{ trimDate(post.dropoffDate) }}</td>
      </tr>
    </table>
    <p>Starting Bid: ${{ post.startingBid }}</p>
    <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white ml-5 mr-5 mb-3" @click="showDetailedPost()">Details</button>
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
      this.$router.push({ name: 'viewDetailedCarrierPost', params: { id: this.post.id.toString() } })
    },
    trimDate(date) {
      return new Date(date).toISOString().split('T')[0]
    }
  }
}
</script>