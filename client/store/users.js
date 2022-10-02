import { defineStore } from "pinia";

export const useUsersStore = defineStore({
  id: "users-store",
  state: () => {
    return {
      isLoginOk: false,
      isUserChecked: true,
      userData: null,
    };
  },
  actions: {
    setIsLoginOk(value) {
      this.isLoginOk = value;
    },
    setIsUserChecked(value) {
      this.isUserChecked = value;
    },
    setUserData(value) {
      this.userData = value;
    },
  },
});
