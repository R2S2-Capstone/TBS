<template>
  <div class="container pt-5">
    {{ this.id }}
  </div>
</template>

<script>
export default {
  name: 'detailedShipperPost',
  props: {
    id: String
  },
  methods: {
    getPostById() {
      this.$store.dispatch('posts/getPostById', { type: 'carrier', postId: this.id })
        .then((response) => {
          console.log(response)
        })
        .catch((error) => {
          console.log(JSON.stringify(error))
          // this.error = true
        })
    }
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.getPostById()
  }
}
</script>
