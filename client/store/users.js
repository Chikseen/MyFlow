import { defineStore } from "pinia";
import api from "~~/assets/helper/apiService";

export const useUsersStore = defineStore({
  id: "users-store",
  state: () => {
    return {
      isLoginOk: false,
      isUserChecked: false,
      userData: { name: null },
      isEditMode: false,
      allCounter: null,
      curentCounter: null,
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
    setAllCounter(value) {
      this.allCounter = value;
    },
    setCurrentCounter(value) {
      const router = useRoute();
      const id = router.params.id;
      if (id) this.curentCounter = this.allCounter.find((counter) => counter.id == id);
    },
  },
  getters: {
    async getAllCounter(state) {
      state.allCounter = await api.get("numbers"); // refresh
      return state.allCounter;
    },
  },
});
