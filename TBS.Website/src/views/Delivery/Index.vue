<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col-12">
        
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'deliveryIndex',
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