<template>
  <div class="form-label-group">
    <label>Trailer Type</label>                    
    <select v-model="input" class="form-control text-center">
      <option v-for="(value, index) in [ 'Enclosed', 'Flat bed', 'Car Carrier', 'Other' ]" :key="index" :value="value" selected>
        {{ value }}
      </option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'trailerInput',
  props: {
    value: {
      type: String,
      required: false,
      default: 'Car Carrier'
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