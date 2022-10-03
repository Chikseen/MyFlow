<template>
  <div class="mobileWrapper">
    <UserHeader />
    <NuxtPage />
  </div>
</template>

<script>
import UserHeader from '~~/components/user/UserHeader.vue';

export default {
  components: {
    UserHeader
  },
  data() {
    return {}
  },
  computed: {}
}
</script>

<style lang="scss">
.mobileWrapper {
  position: relative;
}
</style>
