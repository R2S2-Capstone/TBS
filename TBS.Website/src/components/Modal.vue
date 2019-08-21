<template>
  <div class="background">
    <div class="modal-content">
      <div class="modal-header">
        <h2>{{ title }}</h2>
      </div>
      <div class="modal-body">
        <div class="row">
            {{ description }}
        </div>
      </div>
      <div class="modal-footer">
        <div class="row">
            <button @click="cancel" type="button" class="btn btn-secondary m-2">Cancel</button>
            <button @click="submit" type="button" class="btn btn-primary m-2" data-dismiss="modal">{{ submitText  }}</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'modal',
  props: {
    title: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    submitText: {
      type: String,
      required: false,
      default: 'Confirm',
    },
    submit: {
      type: Function,
      required: true,
    },
    cancel: {
      type: Function,
      required: true,
    }
  }
}
</script>

<style lang="scss" scoped>
.background {
  background-color: rgba(0, 0, 0, 0.5);
  position: fixed;
  left: 0;
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  top: 0;
  z-index: 1;
}
.modal-content {
  background-color: #fff;
  position: fixed;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  width: auto;
  align-items: center;
  z-index: 1;
}
</style>
