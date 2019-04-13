<template>
  <div>
    <Spinner v-if="loading"/>
    <TheHeader />
    <router-view />
    <TheFooter />
  </div>
</template>

<script>
import Spinner from '@/components/Spinner.vue'
import TheHeader from '@/components/Navigation/Header.vue'
import TheFooter from '@/components/Navigation/Footer.vue'

export default {
  name: 'app',
  components: {
    Spinner,
    TheHeader,
    TheFooter
  },
  computed: {
    loading() {
      return this.$store.getters['global/isLoading']
    }
  }
}
</script>


<style lang="scss">
@import "@/assets/scss/global.scss";
* {
  padding: 0;
  margin: 0;
  font-family: "Montserrat", sans-serif;
  color: colour(mainText);
}
body {
  background-color: colour(body);
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
.font-blue-on-hover {
  &:hover {
    color: colour(colourPrimary);
    opacity: 0.75;
  }
}
.bg-blue {
  background-color: colour(colourPrimary);
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
.fa-star {
	color: #efce4a;
}
</style>
