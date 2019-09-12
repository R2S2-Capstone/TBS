<template>
  <div>
    <div class="container">
    </div>
    <div class="container-fluid">
      <div class="container">
        <div class="row pt-5 pb-5 mb-5">
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
          title: 'Question 1',
          answer: 'Answer 1'
        },
        {
          title: 'Question 2',
          answer: 'Answer 2'
        },
        {
          title: 'Question 3',
          answer: 'Answer 3'
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
