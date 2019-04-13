<template>
  <div class="container pt-5">
    <div class="row">
      <div class="row full-page">
      </div>
      <div class="row">
        <div class="col-lg-8 col-md-8 col-sm-12">
          <img src="https://images.pexels.com/photos/826349/pexels-photo-826349.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260" class="img-fluid shipper" alt="">
        </div>
        <div class="col">
          <p class="sub-heading pt-2">Ship your vehicle</p>
          <h3 class="heading">Find a carrier at a reasonable price</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
          <router-link class="how-link pb-2" :to="{ name: 'home' }">How it works for shippers <i class="fas fa-arrow-right"></i></router-link>
        </div>
      </div>
      <div class="row pt-5 pb-5">
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2">
          <p class="sub-heading pt-2">Available truck space</p>
          <h3 class="heading">Directly connect with shippers</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
          <router-link class="how-link" :to="{ name: 'home' }">How it works for carriers <i class="fas fa-arrow-right"></i></router-link>
        </div>
        <div class="col">
          <img src="https://www.helpinc.us/wp-content/uploads/2018/03/truck-driver-tablet-1024x683.jpg" class="img-fluid carrier" alt="">
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'home',
  components: {

  }
}
</script>

<style lang="scss">
@import "@/assets/scss/global.scss";
// .full-page {
//   height: 81vh;
// }
.sub-heading {
  margin: 0;
  font-size: 0.85rem;
  font-weight: bold;
}
.heading {
  font-weight: bold;
}
.how-link {
  color: inherit;
  border-bottom: 0.2rem solid colour(colourPrimary);
  &:hover {
    
  text-decoration: none;
    color: colour(colourPrimary);
  }
}
.carrier {
  clip-path: polygon(50% 0, 100% 0, 100% 100%, 0% 100%);
}
.shipper {
  clip-path: polygon(0 0, 50% 0, 100% 100%, 0% 100%);
}
</style>

