import { useUsersStore } from "~/store/users";

const apiService = {
  async getUser(adress) {
    const config = useRuntimeConfig();
    const request = await fetch(`${config.API_BASE}/${adress}`, {
      credentials: "include",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      method: "GET",
      mode: "cors",
      redirect: "follow",
    });
    return request;
  },
  async get(adress) {
    const config = useRuntimeConfig();
    const request = await fetch(`${config.API_BASE}/${adress}`, {
      credentials: "include",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      method: "GET",
      mode: "cors",
      redirect: "follow",
    });
    return await this.checkAuth(request);
  },
  async post(adress, payload) {
    const config = useRuntimeConfig();
    const request = await fetch(`${config.API_BASE}/${adress}`, {
      credentials: "include",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      method: "POST",
      mode: "cors",
      redirect: "follow",
      body: JSON.stringify(payload),
    });
    return await this.checkAuth(request);
  },
  async put(adress, payload) {
    const config = useRuntimeConfig();
    const request = await fetch(`${config.API_BASE}/${adress}`, {
      credentials: "include",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      method: "PUT",
      mode: "cors",
      redirect: "follow",
      body: JSON.stringify(payload),
    });
    return await this.checkAuth(request);
  },
  async delete(adress, payload) {
    const config = useRuntimeConfig();
    const request = await fetch(`${config.API_BASE}/${adress}`, {
      credentials: "include",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      method: "DELETE",
      mode: "cors",
      redirect: "follow",
      body: JSON.stringify(payload),
    });
    return await this.checkAuth(request);
  },

  async checkAuth(res) {
    if (res.status >= 400) {
      const usersStore = useUsersStore();
      usersStore.$reset();
      return null;
    }
    try {
      return await res.json();
    } catch (e) {
      return res;
    }
  },
};

export default apiService;
