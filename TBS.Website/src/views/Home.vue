<template>
  <div class="mb-5">
    <div class="container">
      <div class="row pt-5 pb-5">
        <div class="col-lg-8 col-md-8 col-sm-12">
          <img src="https://static.reecerose.com/images/tbs/shipper.jpeg" class="img-fluid shipper" alt="">
        </div>
        <div class="col">
          <p class="sub-heading pt-2">Ship your vehicle</p>
          <h3 class="heading">Connect with a Carrier</h3>
          <hr>
          <p>Instantly connect with a carrier that can move your vehicle when you want, where you want and for the right price</p>
          <router-link class="how-link pb-2" :to="{ name: 'aboutShipper' }">Find out more <i class="fas fa-arrow-right"></i></router-link>
        </div>
      </div>
    </div>
    <div class="container-fluid bg-white">
      <div class="container">
        <div class="row pt-5 pb-5">
          <div class="col-lg-4 col-md-4 col-sm-12 pb-2">
            <p class="sub-heading pt-2">Fill your empty trailer spaces</p>
            <h3 class="heading">Find freight now</h3>
            <hr>
            <p>Review and accept freight to instantly fill up your trailer</p>
            <router-link class="how-link pb-2" :to="{ name: 'aboutCarrier' }">Find out more <i class="fas fa-arrow-right"></i></router-link>
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
          <p>Post as many times as you want without worry</p>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2 text-center">
          <i class="fas fa-gavel fa-4x pb-3 text-blue"></i>
          <h3 class="heading">Competitive Bidding</h3>
          <hr>
          <p>Ensure you get the best price for your business</p>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12 pb-2 text-center">
          <i class="fas fa-mobile-alt fa-4x pb-3 text-blue"></i>
          <h3 class="heading">Available Everywhere</h3>
          <hr>
          <p>We are accessible from anywhere! No matter where you are, we provide easy access to carriers and shippers who can help your business</p>
        </div>
      </div>
    </div>
    <div class="container-fluid bg-white pb-1">
      <div class="container">
        <div class="row pt-5 pb-5 mb-5">
          <div class="col">
            <p class="sub-heading pt-2">Transportation Bidding System</p>
            <h3 class="heading">What We Do</h3>
            <hr>
            <p>We help our customers to connect with each other to fulfill their needs</p>
            <router-link class="how-link" :to="{ name: 'aboutUs' }">About Us <i class="fas fa-arrow-right"></i></router-link>
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

