<template>
  <footer class="fixed-bottom bg-white">
    <div class="row">
      <div class="col-12">
        <div class="text-center p-3">© {{ new Date().getFullYear() }} Copyright:
          <router-link :to="{ name: 'home' }" class="fade-on-hover"> TBS Inc.</router-link> and its licensors. All rights reserved.
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

<style lang="scss">
footer {
  border-top: 1px solid rgba(0, 0, 0, 0.19);
	// box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 0 3px 8px rgba(0, 0, 0, 0.19);
}
</style>
