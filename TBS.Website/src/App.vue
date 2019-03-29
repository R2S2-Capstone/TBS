<template>
  <div>
    <TheHeader />
    <router-view />
    <TheFooter />
  </div>
</template>

<script>
import TheHeader from "@/components/Navigation/Header.vue";
import TheFooter from "@/components/Navigation/Footer.vue";
export default {
  name: "app",
  components: {
    TheHeader,
    TheFooter
  }
};
</script>


<style lang="scss">
@import "@/assets/scss/global.scss";
* {
  padding: 0;
  margin: 0;
  font-family: "Montserrat", sans-serif;
  color: color(mainText);
}
body {
  @include mobile {
    margin-bottom: 80px;
  }
  margin-bottom: 56px;
}
.fade-on-hover {
  &:hover {
    opacity: 0.75;
  }
}
.bg-blue {
  background-color: color(colorPrimary);
}
.btn-main {
  font-size: 0.75rem;
  border-radius: 5rem;
  letter-spacing: 0.1rem;
  font-weight: bold;
  padding: 1rem;
}
.pointer {
  cursor: pointer;
}
</style>
