<template>
  <div class="form-label-group text-center">
    <label>Time</label>
    <input
      v-model="input"
      type="time" 
      id="DateInput" 
      class="form-control"
    >
  </div>
</template>

<script>
export default {
  name: 'timeInput',
  props: {
    value: {
      type: String,
      required: false,
      default: (new Date().getHours() + ":" + new Date().getMinutes()).toString()
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
  },
}
</script>