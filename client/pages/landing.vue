<template>
    <div>
        <h1>MyFlow</h1>
        <p>Store all your monthly numbers</p>
        <nuxt-link to="overview">OVERVIEW</nuxt-link>
        <button @click="createCounter">CREATE</button> <!-- IDK WHY -->
    </div>
</template>

<script>
import api from '~~/apiService';

export default {
    methods: {
        async createCounter() {
            console.log("hi");
            const res = await api.post("numbers", { name: "hi" });
            console.log(res);
        }
    },
}
</script>