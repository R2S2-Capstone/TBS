<template>
  <footer class="fixed-bottom bg-blue">
    <div class="row">
      <div class="col-12">
        <div class="text-center p-3">© {{ new Date().getFullYear() }} Copyright:
          <router-link :to="{ name: 'home' }" class="text-white fade-on-hover"> TBS Inc.</router-link> and its licensors. All rights reserved.
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'navigationFooter'
}
</script>
