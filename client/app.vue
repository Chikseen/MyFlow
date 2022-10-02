<template>
  <div>
    <Default />
  </div>
</template>

<script>
import { useUsersStore } from '~/store/users'
import api from '~~/apiService'

import Default from "~/layouts/default.vue"

export default {
  components: {
    Default
  },
  async mounted() {
    const usersStore = useUsersStore();
    usersStore.setIsUserChecked(true);

    const loginState = await api.getUser("auth/checkuser");
    if (loginState.status === 200) {
      usersStore.setIsLoginOk(true);
      const userData = await loginState.json();
      console.log(userData)
      usersStore.setUserData(userData);
    }
    usersStore.setIsUserChecked(false);
  }
}
</script>

<style lang="scss">
html {
  margin: 0;
  padding: 0;
}

body {
  font-family: "Sofia", sans-serif;
  margin: 0;
  padding: 0;
}
</style>