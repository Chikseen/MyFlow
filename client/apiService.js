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
};

export default apiService;
