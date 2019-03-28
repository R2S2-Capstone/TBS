<template>
  <div class="container pt-5">
    <WideCard title="Create Post">
      <div slot="card-content" class="text-center">
        <form @submit.prevent="submit">
          <div class="row">
            <div class="col-12">
              <h5>Vehicle Information</h5>
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
                  <input type="text" class="form-control" placeholder="Escape">
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Year</label>
                  <select class="form-control">
                    <option v-for="(value, index) in years()" :key="index" :value="value" selected>
                      {{ value }}
                    </option>
                  </select>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <!-- TODO: Add more vehicle information? -->
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
                <div class="col-12 form-group">
                  <label>Address</label>
                  <input type="text" class="form-control" placeholder="1430 Trafalgar Rd, Oakville, ON L6H 2L1">
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Time</label>
                  <input type="text" class="form-control" placeholder="08:00AM">
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Name</label>
                  <input type="text" class="form-control" placeholder="Jane Doe">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Email</label>
                  <input type="text" class="form-control" placeholder="jane.doe@gmail.com">
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
                <div class="col-12 form-group">
                  <label>Address</label>
                  <input type="text" class="form-control" placeholder="7899 McLaughlin Rd, Brampton, ON L6Y 5H9">
                </div>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Date</label>
                  <input type="text" class="form-control" :placeholder="date.toLocaleDateString()">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Time</label>
                  <input type="text" class="form-control" placeholder="12:00PM">
                </div>
              </div>
              <div class="col-12">
                <p>Contact Information</p>
                <hr>
              </div>
              <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Name</label>
                  <input type="text" class="form-control" placeholder="John Doe">
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 form-group">
                  <label>Email</label>
                  <input type="text" class="form-control" placeholder="john.doe@gmail.com">
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
                  <input type="text" class="form-control" placeholder="1500">
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
  name: 'dealerCreatePost',
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
