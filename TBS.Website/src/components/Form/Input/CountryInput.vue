<template>
  <div class="form-label-group">
    <select v-model="input" class="form-control text-center">
      <option value="Canada">Canada</option>
      <option value="USA">USA</option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'countryInput',
  props: {
    value: {
      type: String,
      required: false,
      default: 'Canada'
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