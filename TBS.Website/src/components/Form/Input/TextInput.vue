<template>
  <div class="form-label-group">
    <label>{{ placeHolder }}</label>
    <input 
      v-model="input"
      @blur="validator.$touch()"
      :class="{ 'is-invalid': validator.$error }"
      type="text" 
      id="TextInput" 
      class="form-control"
      :placeholder="placeHolder" 
      autofocus
    >
    <p v-if="validator.$error" class="text-danger text-center">{{ errorMessage }}</p>
  </div>
</template>

<script>
export default {
  name: 'textInput',
  props: {
    value: {
      type: String,
      required: false,
      default: ""
    },
    errorMessage: {
        type: String,
        required: false,
        default: "Input error"
    },
    placeHolder: {
        type: String,
        required: false,
        default: null
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