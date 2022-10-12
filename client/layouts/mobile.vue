<template>
  <div :class="['mobileWrapper', {'mobileWrapper_setBack':isSetBack}]">
    <Footer @sthExpanded="toggleSetBack" />
    <NuxtPage :class="[{'mobileWrapper_setBack_pages':isSetBack}]" />
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
  transition: all 0.3s;

  &_setBack {
    background-color: rgb(216, 216, 216);
    width: 90%;
    border-radius: 10px;
    margin: 5%;

    &_pages {
      height: calc(100vh - 20px) !important;
    }
  }
}
</style>
