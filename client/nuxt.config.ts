// https://v3.nuxtjs.org/api/configuration/nuxt.config
export default defineNuxtConfig({
  head: {
    htmlAttrs: {
      lang: "en",
    },
    title: "My Flow",
    meta: [
      { charset: "utf-8" },
      { name: "viewport", content: "width=device-width, initial-scale=1" },
      {
        hid: "template",
        name: "template",
        content: "template",
      },
    ],
    link: [{ rel: "icon", type: "image/x-icon", href: "/favicon.ico" }],
  },

  components: true,

  modules: [
    [
      "@pinia/nuxt",
      {
        autoImports: ["defineStore", "acceptHMRUpdate"],
      },
    ],
  ],

  imports: {
    dirs: ["stores"],
  },

  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: '@import "~/assets/scss/global.scss";',
        },
      },
    },
  },

  // PWA module configuration: https://go.nuxtjs.dev/pwa
  pwa: {
    manifest: {
      lang: "en",
    },
  },

  styleResources: {
    scss: ["./assets/scss/*.scss"],
  },

  runtimeConfig: {
    public: {
      GITHUB_CLIENT_ID: "",
      GOOGLE_CLIENT_ID: "",
      GOOGLE_REDIRECT_URI: "",
      API_BASE: "",
    },
  },
});
