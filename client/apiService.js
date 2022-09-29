const apiService = {
  async get(adress) {
    const request = await fetch(
      `${process.env.VUE_APP_API}/${adress}`,
      {
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
        },
        method: 'GET',
        mode: 'cors',
        redirect: 'follow',
      }
    )
    return await request.json()
  },
}

export default apiService
