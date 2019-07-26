<template>
<div>
  <ShipperPost v-if="showShipper" :post="post" />
  <CarrierPost v-else :post="post" />
</div>
</template>

<script>
import ShipperPost from '@/components/Card/ShipperPost.vue'
import CarrierPost from '@/components/Card/CarrierPost.vue'

export default {
  name: 'post',
  components: {
    ShipperPost,
    CarrierPost,
  },
  props: {
    post: Object
  },
  computed: {
    showShipper() {
      return this.$store.getters['authentication/getAccountType'] == 'Carrier' ? true : false
    }
  }
}
</script>
