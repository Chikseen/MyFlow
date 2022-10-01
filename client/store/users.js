import { defineStore } from "pinia";

export const useUsersStore = defineStore({
  id: "users-store",
  state: () => {
    return {
      isLoginOk: false,
      isUserChecked: false,
    };
  },
  actions: {
    setIsLoginOk(value) {
      this.isLoginOk = value;
    },
    setIsUserChecked(value) {
      this.isUserChecked = value;
    },
  },
});
