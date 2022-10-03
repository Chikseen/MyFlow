import { useUsersStore } from "~/store/users";

export default defineNuxtRouteMiddleware((to, from) => {
  const usersStore = useUsersStore();
  if (usersStore.isLoginOk) {
    return;
  }
  return abortNavigation();
});
