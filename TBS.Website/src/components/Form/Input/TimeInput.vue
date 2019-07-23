<template>
  <div class="form-label-group text-center">
    <input 
      v-model="input"
      type="time" 
      id="DateInput" 
      class="form-control test"
    >
  </div>
</template>

<script>
export default {
  name: 'timeInput',
  data() {
    return {
      time: new Date().getHours() + ":" + new Date().getMinutes()
    }
  },
  computed: {
    input: {
      get() {
        return this.time
      },
      set(value) {
        this.$emit("input", value)
      }
    },
  },
}
</script>