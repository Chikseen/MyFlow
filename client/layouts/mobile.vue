<template>
  <div :class="['mobileWrapper', {'mobileWrapper_setBack':isSetBack}]">
    <Footer @sthExpanded="toggleSetBack" />
    <NuxtPage />
  </div>
</template>

<script>
import Footer from '~~/components/footer/Footer.vue';

export default {
  components: {
    Footer
  },
  data() {
    return {
      isSetBack: false
    }
  },
  methods: {
    toggleSetBack(e) {
      this.isSetBack = e;
      if (e)
        document.body.style.height = "calc(100% - 20px)"
    }
  },
  computed: {}
}
</script>

<style lang="scss">
.mobileWrapper {
  background-color: white;
  margin: 0;
  width: 100%;
  height: 100%;
  transition: all 0.3s;

  &_setBack {
    background-color: #d8d8d8;
    width: 90%;
    border-radius: 10px;
    margin: 5%;
  }
}
</style>
