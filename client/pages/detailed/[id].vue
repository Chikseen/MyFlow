<template>
    <div>
        <button @click="$router.push('/overview')">Overview</button>
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
        <div>
            DATA DISPLAY:
            <span>
                <label for="">values</label>
                <p>{{values}}</p>
            </span>
            <span>
                <label for="">aremeticCenter</label>
                <p>{{aremeticCenter}}</p>
            </span>
            <span>
                <label for="">Days min:</label>
                <p>{{minDay}}</p>
                <label for="">Days max:</label>
                <p>{{maxDay}}</p>
                <label for="">Days diff:</label>
                <p>{{daysDiff}}</p>
            </span>
        </div>
        <div v-for="(item, index) in numbers" :key="index">
            <p class="TEMP_CLICKABLE" @click="removeNumber(item.id)">{{item}}</p>
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
    computed: {
        today() {
            return new Date().toISOString().split('T')[0]
        },
        /* START HERE */
        values() {
            if (this.numbers)
                return this.numbers.map((number) => number = number.value)
            return null
        },
        dates() {
            if (this.numbers)
                return this.numbers.map((number) => new Date(number = number.date).getTime())
            return null
        },
        minDay() {
            if (this.dates)
                return new Date(Math.min(...this.dates)).toISOString();
            return null
        },
        maxDay() {
            if (this.dates)
                return new Date(Math.max(...this.dates)).toISOString();
            return null
        },
        daysDiff() {
            return Math.round((new Date(this.maxDay).getTime() - new Date(this.minDay).getTime()) / (1000 * 3600 * 24));
        },
        aremeticCenter() {
            if (this.values) {
                let center = 0;
                this.values.forEach(value => {
                    center += value
                });
                return center / this.values.length;
            }
            return null
        },
        /* START HERE */
        // this should be ONE obj
    },
    methods: {
        setNewDate(e) {
            this.newDate = e.target.value
        },
        async getDetailedNumbers() {
            const res = await api.get(`numbers/${this.$route.params.id}`);
            if (res === null)
                this.$router.push('/landing')
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
            this.numbers.push(res);
        },
        async removeNumber(id) {
            const res = await api.delete(`numbers/${this.$route.params.id}`, { id: id });
            if (res === null)
                this.$router.push('/landing')
            if (res.status === 200)
                this.numbers = this.numbers.filter((numbers) => numbers.id !== id)
        },
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