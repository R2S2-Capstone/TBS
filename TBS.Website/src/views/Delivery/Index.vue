<template>
  <div class="container pt-5">
    <div class="row text-center">
      <div class="col-12">
        <h2 v-if="postType == 'carrier'">Delivery for {{ post.pickupLocation }} -> {{ post.dropoffLocation }}</h2>
        <h2 v-else>Delivery for {{ post.pickupLocation.City }} -> {{ post.dropoffLocation.City }}</h2>
        <h4 v-if="error" class="text-danger mb-5">Failed to load post information</h4>
      </div>
    </div>
    <div class="row pt-2 text-center">
      <div class="col-12">
        <ul class="progress-bar-list d-flex justify-content-center">
          <li :class="(post.postStatus == 'pending delivery' || post.postStatus == 'pending delivery approval' || post.postStatus == 'completed') == true ? 'active' :''">Pending Delivery</li>
          <li :class="(post.postStatus == 'pending delivery approval' || post.postStatus == 'completed') == true ? 'active' : ''">Pending Delivery Approval</li>
          <li :class="(post.postStatus == 'completed') == true ? 'active' : ''">Completed</li>
        </ul>
      </div>
    </div>
    <div class="row pt-3 text-center">
      <div class="col-12 background mb-5 pt-3">
        <h4>Post Details</h4>
        <hr>
        <div class="row">
          <div class="col-12">
            <p></p>
            <p>
             Details
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'delivery',
  data() {
    return {
      accountType: '',
      postType: 'carrier',
      post: {
        pickupLocation: 'Oakville',
        dropoffLocation: 'Montreal',
        postStatus: 'completed'
      },
      error: null,
    }
  },
  methods: {
    loadDetails() {
      //TODO: Load post/bid/review details
      //TODO: set postType
    }
  },
  props: {
    id: String
  },
  created() {
    if (this.id == null) this.$router.go(-1)
    this.accountType = this.$store.getters['authentication/getAccountType'].toLowerCase()
    this.loadDetails()
  }
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss";

.progress {
  height: auto;
  .progress-bar {
    h6 {
      font-weight: 500;
    }
  }
}
.background {
  background-color: #fff;
  border: 1px solid #dbdbdb;
  border-radius: 7.5px;
}
hr {
  margin: 0 auto;
  width: 15vw;
  max-width: 100%;
  border-top: 1px solid colour(colourPrimary);
}

.progress-bar-list {
  counter-reset: step;
  li {
    list-style-type: none;
    width: 25%;
    float: left;
    font-size: 12px;
    position: relative;
    text-align: center;
    text-transform: uppercase;
    color: #7d7d7d;

    &::before {
      width: 30px;
      height: 30px;
      content: counter(step);
      // padding-bottom: 5px;
      counter-increment: step;
      line-height: 30px;
      border: 2px solid #7d7d7d;
      display: block;
      text-align: center;
      margin: 0 auto 10px auto;
      border-radius: 50%;
      background-color: white;
    }

    &:after {
      width: 100%;
      height: 2px;
      content: '';
      position: absolute;
      background-color: #7d7d7d;
      top: 15px;
      left: -50%;
      z-index: -1;
    }

    &:first-child:after {
      content: none;
    }
  }

  .active {
    color: green;

    &:before {
      border-color: #55b776;
    }
    &:after {
      background-color: #55b776;
    }
  }
}
</style>