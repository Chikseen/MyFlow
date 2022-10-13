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
    setCurrentCounter() {
      const router = useRoute();
      const id = router.params.id;
      if (id) this.curentCounter = this.allCounter.find((counter) => counter.id == id);
    },
    async fetchAllCounter() {
      this.allCounter = await api.get("numbers");
      this.setCurrentCounter();
    },
  },
  getters: {
    async getAllCounter(state) {
      if (state.allCounter != null) {
        return state.allCounter;
      } else {
        const res = await api.get("numbers");
        if (res === null) this.$router.push("/landing");
        state.allCounter = res;
        return res;
      }
    },
  },
});
