<template>
  <div>
    <div class="container">
    </div>
    <div class="container-fluid">
      <div class="container">
        <div class="row pt-5 pb-5 mb-5">
          <div class="col">            
            <h3 class="heading">What We Do For Our Carriers</h3>
            <p class="sub-heading pt-2">Transportation Bidding System</p>
            <hr>
            <p>We help our customers to connect with each other to fulfil their needs</p>
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
</style>

