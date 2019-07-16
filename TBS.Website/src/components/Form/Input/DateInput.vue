<template>
  <div class="form-label-group text-center">
    <input 
      v-model="input"
      type="date" 
      id="DateInput" 
      class="form-control text-center"
      :min="date"
    >
  </div>
</template>

<script>
export default {
  name: 'dateInput',
  data() {
    return {
      date: new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60000 )).toISOString().split('T')[0]
    }
  },
  computed: {
    input: {
      get() {
        return this.date
      },
      set(value) {
        this.$emit("input", value)
      }
    },
  }
}
</script>