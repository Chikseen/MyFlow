<template>
    <div>
        <button @click="$router.push('/overview')">Overview</button>
        <p>{{ numbers }}</p>
    </div>
</template>

<script>
import api from '~~/apiService';

export default {
    data() {
        return {
            numbers: null
        }
    },
    methods: {
        async getDetailedNumbers() {
            const res = await api.get(`numbers/${this.$route.params.id}`);
            if (res === null)
                this.$router.push('/landing')
            console.log(res);
            this.numbers = res;
        }
    },
    mounted() {
        this.getDetailedNumbers();
    },
}
</script>

<!-- found no way for options api :thinking: -->
<script setup>
definePageMeta({
    middleware: 'auth'
})
</script> 