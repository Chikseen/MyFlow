<template>
  <Default />
</template>

<script>
import { mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'
import api from '~~/assets/helper/apiService'

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
html,
body {
  margin: 0;
  padding: 0;
  height: 100%;
  font-family: "Merienda", Helvetica, Arial;
  margin: 0;
  padding: 0;
  background-color: #373737;
}

#__nuxt {
  height: 100%;
}

h1,
h2 {
  margin: 0;
}

input {
  width: 90%;
  padding: 0;
  border: 0.1px rgb(135, 135, 135) solid;
  border-radius: 25px;
}

button {
  width: 90%;
  padding: 0;
  border-radius: 25px;
  border: none;
  background-color: none;
}

.box-border {
  box-shadow: 0px 0px 7px #2e2e2e7a;
  border-radius: 5px;
}

.IconBase {}
.LogoBase {
  border-radius: 3px;
  box-shadow: 0px 0px 5px #2e2e2e86;
  max-height: 35px;
}
</style>