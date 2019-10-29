<template>
    <div class="container pt-5 pb-5">
      <div class="row">
        <div class="col">            
          <h3 class="heading">FAQ</h3>
          <p class="sub-heading pt-2">Transportation Bidding System</p>
          <hr>
          <div class="accordion" id="faqAccordion">
            <div class="card" v-for="(question, index) in questions" :key="index">
              <div class="card-header" :id="'heading'+index">
                <h2 class="mb-0">
                  <button class="btn btn-link" type="button" data-toggle="collapse" :data-target="'#collapse'+index" aria-expanded="true" :aria-controls="'collapse'+index">
                    {{ question.title }}
                  </button>
                </h2>
                </div>
                <div :id="'collapse'+index" class="collapse" :aria-labelledby="'heading'+index" data-parent="#faqAccordion">
                  <div class="card-body">
                  {{ question.answer }}
                </div>
              </div>
            </div>
          </div>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'faq',
  data() {
    return {
      questions: [
        {
          title: 'What costs are associated with using this product?',
          answer: 'There is a two dollar surcharge when purchasing a service using our platform from either a carrier or a shipper. That is the only cost!'
        },
        {
          title: 'Why should we use you over other shipping websites?',
          answer: 'Our website is specialized specifically for people looking to ship vehicles and has features specifically to help with that.'
        },
        {
          title: 'If I have a dispute with someone I purchased a service from, What do I do?',
          answer: 'You will need to utilize the Paypal dispute resolution centre to resolve any issues with repayment. Otherwise, you can contact the seller directly using the email attached to the bid.'
        },
      ]
    }
  }
}
</script>

<style lang="scss">
.heading {
  font-weight: bold;
}
.sub-heading {
  text-transform: uppercase;
  margin: 0;
  font-size: 0.80rem;
  font-weight: bold;
}
</style>
