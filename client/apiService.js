const apiService = {
  async get(adress) {
    const config = useRuntimeConfig();
    console.log(config);
    const request = await fetch(`${config.API_BASE}/${adress}`, {
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
};

export default apiService;
