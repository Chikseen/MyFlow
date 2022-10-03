<template>
    <div>
        <label for="">Create new Counter</label>
        <div>
            <label for="">Name:</label>
            <input type="text" v-model="createCoutnerText">
            <button @click="createCounter">CREATE</button>
        </div>
    </div>
</template>

<script>
import api from '~~/apiService';

export default {
    data() {
        return {
            createCoutnerText: ""
        }
    },
    methods: {
        async createCounter() {
            const res = await api.post("numbers", { name: this.createCoutnerText });
            console.log(res);
        }
    },
}
</script>
    
<!-- found no way for options api :thinking: -->
<script setup>
definePageMeta({
    middleware: 'auth'
})
</script> 
    