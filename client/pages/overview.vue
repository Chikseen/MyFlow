<template>
    <div>
        <button @click="createCounter">CREATE</button>
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