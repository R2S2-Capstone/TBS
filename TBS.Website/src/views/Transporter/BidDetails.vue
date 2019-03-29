<template>
  <div class="container pt-5">
    <WideCard :title="'Bid from ' + bid.bidder.name">
      <div slot="card-content" class="text-center">
        <div class="row">
          <div class="col-12">
            <hr>
            <h4>Bid Information</h4>
            <hr>
          </div>
          <div class="col-12 text-left">
            <div class="row">
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-money-bill"></i> Bid Amount:</p></div>
                  <div class="col text-right">{{ format(bid.amount) }}</div>
                </div>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-star"></i> Bidder Rating:</p></div>
                  <div class="col text-right">{{ bid.bidder.rating }} <i class="fas fa-star"></i></div>
                </div>
              </div>
            </div>
            <hr>
            <h5 class="text-center">Pickup Information</h5>
            <hr>
            <div class="row">
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-city"></i> Pickup City:</p></div>
                  <div class="col text-right">{{ bid.pickupCity }}</div>
                </div>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-calendar-day"></i> Pickup Date:</p></div>
                  <div class="col text-right">{{ bid.pickupDate }}</div>
                </div>
              </div>
            </div>
            <hr>
            <h5 class="text-center">Delivery Information</h5>
            <hr>
            <div class="row">
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-city"></i> Delivery City:</p></div>
                  <div class="col text-right">{{ bid.deliveryCity }}</div>
                </div>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-calendar-day"></i> Delivery Date:</p></div>
                  <div class="col text-right">{{ bid.deliveryDate }}</div>
                </div>
              </div>
            </div>
            <hr>
            <h5 class="text-center">Vehicle Description</h5>
            <hr>
            <div class="row">
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-car"></i> Vehicle Make:</p></div>
                  <div class="col text-right">{{ bid.vehicle.make }}</div>
                </div>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-car"></i> Vehicle Model:</p></div>
                  <div class="col text-right">{{ bid.vehicle.model }}</div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-car"></i> Vehicle Year:</p></div>
                  <div class="col text-right">{{ bid.vehicle.year }}</div>
                </div>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="row">
                  <div class="col"><p><i class="fas fa-car"></i> Other</p></div>
                  <div class="col text-right">{{ bid.vehicle.other }}</div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-12">
            <div class="row">
                <div class="col-12 pb-3">
			            <button class="btn btn-main btn- bg-blue fade-on-hover text-uppercase text-white" type="submit">Accept Bid</button>
                </div>
                <div class="col-12">
			            <button class="btn btn-main bg-blue fade-on-hover text-uppercase text-white" type="submit">Decline Bid</button>
                </div>
              </div>
          </div>
        </div>
      </div>
    </WideCard>
  </div>
</template>

<script>
import WideCard from '@/components/Card/WideCard.vue'

export default {
  name: 'transporterViewBidDetails',
  components: {
    WideCard
  },
  beforeCreate() {
    // A post ID must be passed, if not return to previous route
    if (this.$route.params.id == null) this.$router.go(-1)
  },
  data() {
    return {
      bid: {
        id: '1',
        bidder: {
          name: 'Reece Rose',
          rating: 5,
        },
        pickupCity: 'Oakville',
        pickupDate: '03/29/2018',
        deliveryCity: 'Brampton',
        deliveryDate: '03/29/2018',
        vehicle: {
          make: 'Ford',
          model: 'Escape',
          year: '2019',
          other: null
        },
        amount: 5000,
      },
    }
  },
  methods: {
    acceptBid(bidId) {
      this.post.acceptedBid = true
    },
    declineBid(bidId) {
      console.log(`Decline bid ${bidId}`)
    },
    format(number) {
      return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    },
  }
}
</script>