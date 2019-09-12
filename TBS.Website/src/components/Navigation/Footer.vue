<template>
  <footer class="fixed-bottom bg-white">
    <div class="row">
      <div class="col-12">
        <div class="text-center mt-3">
          <router-link :to="{ name: 'faq' }" class="fade-on-hover">FAQ</router-link> | 
          <a href="mailto:tbs@reecerose.com" class="fade-on-hover"> Contact Us</a> |
          <router-link :to="{ name: 'privacy' }" class="fade-on-hover"> Privacy Policy</router-link> 

        </div>
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
	// box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 0 3px 8px rgba(0, 0, 0, 0.19);
  border-top: 1px solid rgba(0, 0, 0, 0.19);
}
</style>
