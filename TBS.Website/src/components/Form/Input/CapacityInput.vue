<template>
  <div class="form-label-group">
    <label>Spaces Available</label>                    
    <select v-model="input" class="form-control text-center">
      <option v-for="(value, index) in [ 1, 2, 3, 4, 5, 6, 7, 8, 9 ]" :key="index" :value="value" selected>
        {{ value }}
      </option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'capacityInput',
  props: {
    value: {
      type: Number,
      required: false,
      default: 1
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