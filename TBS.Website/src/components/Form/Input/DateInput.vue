<template>
  <div class="form-label-group text-center">
    <label>{{ label }}</label>
    <input 
      v-model="input"
      type="date" 
      id="DateInput" 
      class="form-control text-center"
      :min="value"
    >
  </div>
</template>

<script>
export default {
  name: 'dateInput',
  props: {
    label: {
      type: String,
      required: false,
      default: 'Date'
    },
    value: {
      type: String,
      required: false,
      default: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0]
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
    },
  }
}
</script>