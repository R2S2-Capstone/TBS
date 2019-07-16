<template>
  <div class="form-label-group">
    <label>Condition</label>
    <select v-model="input" class="form-control text-center">
      <option v-for="(value, index) in ['New', 'Used']" :key="index" :value="value" selected>
        {{ value }}
      </option>
    </select>
  </div>
</template>
<script>
export default {
  name: 'conditionInput',
  props: {
    value: {
      type: String,
      required: false,
      default: "New"
    },
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