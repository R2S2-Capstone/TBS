<template>
  <div class="container pt-5">
    <WideCard title="Create Availability Post">
      <div slot="card-content" class="text-center">
        <form @submit.prevent="submit">
          <div class="row">
            <div class="col-12">
              <h5>Your Vehicle Information</h5>
              <!-- This should be auto loaded -->
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Make</label>
                  <input type="text" class="form-control" placeholder="Ford">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Model</label>
                  <input type="text" class="form-control" placeholder="F-250">
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Avaialable Vehicle Capacity</label>
                  <input type="text" class="form-control" placeholder="2">
                </div>
                  <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Year</label>
                  <select class="form-control">
                    <option v-for="(value, index) in years()" :key="index" :value="value" selected>
                      {{ value }}
                    </option>
                  </select>
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-5">
            <div class="col-12">
              <h5>Pickup</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>City</label>
                  <input type="text" class="form-control" placeholder="Oakville">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-5">
            <div class="col-12">
              <h5>Delivery</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>City</label>
                  <input type="text" class="form-control" placeholder="Oakville">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
              </div>
            </div>
          </div>
          <div class="row pt-5">
            <div class="col-12">
              <h5>Other Details</h5>
              <hr>
            </div>
            <div class="col-12">
              <div class="row">
                <div class="col-12 form-group">
                  <label>Starting Bid</label>
                  <input type="text" class="form-control" placeholder="2000">
                </div>
                <div class="col-12">
			            <button class="btn btn-main btn- bg-blue fade-on-hover text-uppercase text-white" type="submit">Post</button>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
    </WideCard>
  </div>
</template>

<script>
import WideCard from '@/components/Card/WideCard.vue'

export default {
  name: 'transporterCreatePost',
  components: {
    WideCard,
  },
  data() {
    return {
      date: new Date(),
    }
  },
  methods: {
    years() {
      let items = [];
      for(let year = 1908; year <= this.date.getFullYear(); year++) {
        items.push(year);
      }
      return items;
    },
    submit() {
      // TODO: Submit
    }
  }
}
</script>

<style lang="scss" scoped>
input, input::placeholder {
  text-align: center;
  text-align-last: center;
}
</style>