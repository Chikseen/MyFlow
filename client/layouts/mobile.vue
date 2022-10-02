<template>
  <div class="mobileWrapper">
    <UserHeader />
    <Landing />
  </div>
</template>

<script>
import Landing from '~/pages/landing.vue';
import UserHeader from '~~/components/user/UserHeader.vue';

export default {
  components: {
    Landing,
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
