<template>
    <div>
        <div>
            <p>Counters name: </p>
            <input type="text" v-model="newName" :placeholder="currentCounter.name">
        </div>
        <div>
            <p>Counters unit: </p>
            <input type="text" v-model="newUnit" :placeholder="currentCounter.unit">
        </div>
        <button @click="updateCounter">Save</button>
        Setting
        {{currentCounter}}
    </div>
</template>

<script>
import api from '~~/assets/helper/apiService';
import { mapState } from 'pinia'
import { useUsersStore } from '~/store/users'

export default {
    data() {
        return {
            newUnit: null,
            newName: null,
        }
    },
    computed: {
        ...mapState(useUsersStore, {
            currentCounter: "curentCounter",
            getAllCounter: "getAllCounter"
        })
    },
    methods: {
        async updateCounter() {
            const res = await api.put(`numbers/${this.currentCounter.id}`, {
                name: this.newName || this.currentCounter.name,
                unit: this.newUnit || this.currentCounter.unit,
            });
            if (res === null)
                this.$router.push('/landing');
            this.getAllCounter;
        }
    },
    mounted() {
    },
}
</script> 

<style lang="scss">

</style>