<template>
    <div>
        <button @click="$router.push('/overview')">Overview</button>
        <p>{{ numbers }}</p>
        <div class="detailed_newData">
            <label for="">Add new Data: </label>
            <span>
                <label for="">Value: </label>
                <input type="number" v-model="newValue">
            </span>
            <span>
                <label for="">Date: </label>
                <input type="date" :value="newDate || today" @change="setNewDate">
            </span>
            <button @click="saveNewEntry">Add</button>
        </div>
        <div v-for="(item, index) in numbers" :key="index">
            <p>{{item}}</p>
        </div>
    </div>
</template>

<script>
import api from '~~/apiService';

export default {
    data() {
        return {
            numbers: null,
            newValue: "",
            newDate: null,
        }
    },
    methods: {
        setNewDate(e) {
            console.log(e.target.value)
            this.newDate = e.target.value
        },
        async getDetailedNumbers() {
            const res = await api.get(`numbers/${this.$route.params.id}`);
            if (res === null)
                this.$router.push('/landing')
            console.log(res);
            this.numbers = res;
        },
        async saveNewEntry() {
            const res = await api.post(`numbers/${this.$route.params.id}`, {
                id: this.$route.params.id * 1,
                value: this.newValue,
                date: new Date(this.newDate || this.today).toISOString(),
            });
            if (res === null)
                this.$router.push('/landing')
            console.log(res);
            this.numbers = res;
        }
    },
    computed: {
        today() {
            return new Date().toISOString().split('T')[0]
        }
    },
    mounted() {
        this.getDetailedNumbers();
    },
}
</script>

<!-- found no way for options api :thinking: -->
<!-- <script setup>
definePageMeta({
    middleware: 'auth'
})
</script>  -->

<style lang="scss">
.detailed {
    &_newData {
        display: flex;
        flex-direction: column;
        gap: 15px;
    }
}
</style>