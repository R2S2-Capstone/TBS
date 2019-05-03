<template>
  <div>
    <div class="container">
      <div class="row pt-5 pb-5">
        <div class="col-lg-8 col-md-8 col-sm-12">
          <img src="https://static.reecerose.com/images/tbs/shipper.jpeg" class="img-fluid shipper" alt="">
        </div>
        <div class="col">
          <p class="sub-heading pt-2">Ship your vehicle</p>
          <h3 class="heading">Find a carrier at a reasonable price</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
          <router-link class="how-link pb-2" :to="{ name: 'home' }">Find out more <i class="fas fa-arrow-right"></i></router-link>
        </div>
      </div>
    </div>
    <div class="container-fluid bg-white">
      <div class="container">
        <div class="row pt-5 pb-5">
          <div class="col-lg-4 col-md-4 col-sm-12 pb-2">
            <p class="sub-heading pt-2">Fill Your Empty Space</p>
            <h3 class="heading">Directly connect with shippers</h3>
            <hr>
            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
            <router-link class="how-link pb-2" :to="{ name: 'home' }">Find out more <i class="fas fa-arrow-right"></i></router-link>
          </div>
          <div class="col">
            <img src="https://static.reecerose.com/images/tbs/carrier.jpg" class="img-fluid carrier" alt="">
          </div>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="row pt-5 pb-5">
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2 text-center">
          <i class="fas fa-dollar-sign fa-4x pb-3 text-blue"></i>
          <h3 class="heading">Post For Free</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2 text-center">
          <i class="fas fa-gavel fa-4x pb-3 text-blue"></i>
          <h3 class="heading">Competitive Bidding</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2 text-center">
          <i class="fas fa-mobile-alt fa-4x pb-3 text-blue"></i>
          <h3 class="heading">Available Everywhere</h3>
          <hr>
          <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
        </div>
      </div>
    </div>
    <div class="container-fluid bg-white">
      <div class="container">
        <div class="row pt-5 pb-5 mb-5">
          <div class="col">
            <p class="sub-heading pt-2">Transportation Bidding System</p>
            <h3 class="heading">What We Do</h3>
            <hr>
            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur, pariatur numquam rerum nulla dolore totam iusto quasi blanditiis eos.</p>
            <router-link class="how-link" :to="{ name: 'home' }">Find out more <i class="fas fa-arrow-right"></i></router-link>
          </div>
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
.full-page {
  height: 81vh;
}
.sub-heading {
  text-transform: uppercase;
  margin: 0;
  font-size: 0.80rem;
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
  @include tablet {
    clip-path: polygon(50% 0, 100% 0, 100% 100%, 0% 100%);
  }
  @include desktop {
    clip-path: polygon(50% 0, 100% 0, 100% 100%, 0% 100%);
  }
}
.shipper {
  @include tablet {
    clip-path: polygon(0 0, 50% 0, 100% 100%, 0% 100%);
  }
  @include desktop {
    clip-path: polygon(0 0, 50% 0, 100% 100%, 0% 100%);
  }
}
</style>

