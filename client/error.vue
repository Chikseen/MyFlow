<template>
  <div>
    <h1 v-if="error.statusCode === 404">Seite nicht gefunden</h1>
    <h1 v-else>Ein unbekannter Fehler ist aufgetreten</h1>
    <NuxtLink to="/landing">Zurück zur Startseite</NuxtLink>
  </div>
</template>

<script>
export default {
  props: ['error'],
  layout: 'error', // you can set a custom layout for the error page
  mounted() {
    this.$router.push('/landing')
  },
}
</script>