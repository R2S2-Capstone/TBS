<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col-12">
        Bids
        {{ $route.params.id }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'manageTransporterBids',
  beforeCreate() {
    // A post ID must be passed, if not return to previous route
    if (this.$route.params.id == null) this.$router.go(-1)
  }
}
</script>
