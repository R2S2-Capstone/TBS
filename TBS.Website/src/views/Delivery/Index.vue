<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col-12">
        {{ this.id }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'delivery',
  data() {
    return {
      accountType: ''
    }
  },
  methods: {
    loadDetails() {
      //TODO: Load post/bid/review details
    }
  },
  props: {
    id: String
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.accountType = this.$store.getters['authentication/getAccountType'].toLowerCase()
    this.loadDetails()
  }
}
</script>