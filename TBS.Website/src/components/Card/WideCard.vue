<template>
  <div class="row">
    <div class="col-sm-11 col-md-10 col-lg-10 mx-auto">
      <div class="card custom-card my-5">
        <div class="custom-card-body">
          <h5 class="custom-card-title text-center">{{ title }}</h5>
          <slot name="card-content">

          </slot>
          <slot name="card-content-below">

          </slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'wideCard',
  props: {
    title: String
  },
}
</script>

<style lang="scss" scoped>
.custom-card {
  border: 0;
  border-radius: 1rem;
  box-shadow: 0 0.5rem 1rem 0 rgba(0, 0, 0, 0.1);

  .custom-card-title {
    margin-bottom: 2rem;
    font-weight: 300;
    font-size: 1.5rem;
  }

  .custom-card-body {
    padding: 2rem;
  }
}
</style>