import { defineStore } from "pinia";

export const useUsersStore = defineStore({
  id: "users-store",
  state: () => {
    return {
      isLoginOk: false,
      isUserChecked: false,
      userData: { name: null },
      isEditMode: false,
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
    setEditMode(value) {
      this.isEditMode = value;
    },
  },
});
