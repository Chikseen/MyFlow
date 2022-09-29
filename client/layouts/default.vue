<template>
  <div>
    <div>
      <MobileHeader v-if="isMobile" />
      <DesktopHeader v-else />
    </div>
  </div>
</template>

<script>
import DesktopHeader from "~/layouts/desktop.vue";
import MobileHeader from "~/layouts/mobile.vue";

export default {
  components: {
    DesktopHeader,
    MobileHeader
  },
  computed: {
    isMobile() { // need to find a solution later due to some incompatibility
      return true;
    }
  },
};
</script>

<style lang="scss">

</style>
