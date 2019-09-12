<template>
  <CarrierPost v-if="accountType == 'shipper'" :id="id" />
  <ShipperPost v-else :id="id" />
</template>

<script>
import CarrierPost from '@/views/Delivery/CarrierPost.vue'
import ShipperPost from '@/views/Delivery/ShipperPost.vue'

export default {
  name: 'deliveryIndex',
  components: {
    CarrierPost,
    ShipperPost
  },
  data() {
    return {
      accountType: ''
    }
  },
  props: {
    id: String
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.accountType = this.$store.getters['authentication/getAccountType'].toLowerCase()
  }
}
</script>