<template>
  <div>
    <Default />
  </div>
</template>

<script>
import { mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'
import api from '~~/apiService'

import Default from "~/layouts/default.vue"

export default {
  components: {
    Default
  },
  methods: {
    ...mapActions(useUsersStore, {
      setIsUserChecked: 'setIsUserChecked'
    }),
  },
  async mounted() {
    const usersStore = useUsersStore();
    this.setIsUserChecked(true);

    const loginState = await api.getUser("auth/checkuser");
    if (loginState.status === 200) {
      const userData = await loginState.json();
      console.log(userData)
      usersStore.$patch({
        userData: userData,
        isLoginOk: true
      })
    }
    this.setIsUserChecked(false);
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

.TEMP_CLICKABLE {
  background-color: aquamarine;
  cursor: pointer;
}
</style>