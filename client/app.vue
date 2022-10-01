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
    usersStore.setIsLoginOk(loginState.status === 200);
    usersStore.setIsUserChecked(false);
  }
}
</script>