<template>
    <div>
        <label for="">Create new Counter</label>
        <div>
            <label for="">Name:</label>
            <input type="text" v-model="createCoutnerText">
            <button @click="createCounter">CREATE</button>
        </div>
        <p>{{allCounter}}</p>
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
            console.log(res);
            this.allCounter = res;
        }
    },
    mounted() {
        this.getAllCounter();
    },
}
</script>
    
<!-- found no way for options api :thinking: -->
<script setup>
definePageMeta({
    middleware: 'auth'
})
</script> 
    