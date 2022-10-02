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
    return await request.json();
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
    return await request.json();
  },
};

export default apiService;
