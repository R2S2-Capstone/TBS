<template>
  <div class="form-label-group">
    <select v-model="input" class="form-control text-center">
      <option v-for="(value, index) in years()" :key="index" :value="value" selected>
        {{ value }}
      </option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'yearInput',
  props: {
    value: {
      type: String,
      required: false,
      default: new Date().getUTCFullYear().toString()
    }
  },
  methods: {
    years() {
      let items = [];
      for(let year = 1908; year <= new Date().getUTCFullYear()+1; year++) {
        items.push(year);
      }
      return items;
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
    }
  }
}
</script>