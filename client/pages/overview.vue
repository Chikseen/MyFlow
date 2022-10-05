<template>
    <div>
        <label for="">Create new Counter</label>
        <div>
            <label for="">Name:</label>
            <input type="text" v-model="createCoutnerText">
            <button @click="createCounter">CREATE</button>
        </div>
        <div class="temp123" v-for="(counter, index) in allCounter" :key="index">
            <p @click="loadDetailed(counter)" class="TEMP_CLICKABLE"> {{counter}} </p>
            <p @click="removeCounter(counter.id)" class="TEMP_CLICKABLE">X</p>
        </div>
    </div>
</template>

<script>
import api from '~~/apiService';

export default {
    data() {
        return {
            createCoutnerText: "",
            allCounter: null
        }
    },
    methods: {
        loadDetailed(counter) {
            this.$router.push(`detailed/${counter.id}`)
        },
        async createCounter() {
            const res = await api.post("numbers", { name: this.createCoutnerText });
            if (res === null)
                this.$router.push('/landing')
            if (this.allCounter === null)
                this.allCounter = [];
            this.allCounter.push(res);
        },
        async getAllCounter() {
            const res = await api.get("numbers");
            if (res === null)
                this.$router.push('/landing')
            this.allCounter = res;
        },
        async removeCounter(id) {
            const res = await api.delete("numbers", { id: id });
            if (res === null)
                this.$router.push('/landing')
            if (res.status === 200)
                this.allCounter = this.allCounter.filter((counter) => counter.id !== id)
        },
    },
    mounted() {
        this.getAllCounter();
    },
}
</script>
    
<style lang="scss">
.temp123 {
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
}
</style>

<!-- found no way for options api :thinking: -->
<!-- <script setup>
definePageMeta({
    middleware: 'auth'
})
</script>  -->
    