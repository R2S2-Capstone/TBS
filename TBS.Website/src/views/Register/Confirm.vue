<template>
  <div class="container pt-5 pb-5">
    <h3 class="text-center">Registration Complete!</h3>
    <div class="row text-center">
      <div class="col-12 background pt-3 pb-3">
        <p class="bold">Thank you for registering with TBS!</p>
        <p>An activation email has been sent to the email address you provided.</p>
        <router-link :to="{ name: 'login' }" class="btn btn-main btn-lg bg-blue fade-on-hover text-uppercase text-white">Login</router-link>
      </div>
    </div>
  </div>

</template>

<style lang="scss" scoped>
.background {
  background-color: #fff;
  border: 1px solid #dbdbdb;
  border-radius: 7.5px;
}
</style>
