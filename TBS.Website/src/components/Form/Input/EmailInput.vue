<template>
  <div class="form-label-group">
    <label>Email Address</label>
    <input 
      v-model="input"
      @blur="validator.$touch()"
      :class="{ 'is-invalid': validator.$error }"
      type="text" 
      id="inputEmail" 
      class="form-control"
      placeholder="Email Address" 
      autofocus
    >
    <p v-if="validator.$error" class="text-danger text-center">Not a valid email address</p>
  </div>
</template>

<script>
export default {
  name: 'Email',
  props: {
    value: {
      type: String,
      required: false,
      default: ""
    },
    validator: {
      type: Object,
      required: true  
    }
  },
  computed: {
    input: {
      get() {
          return this.value
      },
      set(value) {
          this.$emit("input", value)
      }
    }
  }
}
</script>

<style lang="scss" scoped>
input {
  text-align: center;
  text-align-last: center;
}
</style>