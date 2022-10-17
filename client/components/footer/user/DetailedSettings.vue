<template>
    <div class="detailedSetting">
        <div>
            <p>Counters name: </p>
            <input type="text" v-model="newName" :placeholder="currentCounter.name">
        </div>
        <div>
            <p>Counters unit: </p>
            <input type="text" v-model="newUnit" :placeholder="currentCounter.unit">
        </div>
        <button @click="updateCounter">Change</button>
        <hr>
        <button @click="toggleEditMode">Remove Numbers</button>
    </div>
</template>

<script>
import api from '~~/assets/helper/apiService';
import { mapState, mapActions } from 'pinia'
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
            allCounter: "allCounter",
            isEditMode: "isEditMode"
        })
    },
    methods: {
        toggleEditMode() {
            this.setEditMode(!this.isEditMode);
            this.$emit("close");
        },
        async updateCounter() {
            const res = await api.put(`numbers/${this.currentCounter.id}`, {
                name: this.newName || this.currentCounter.name,
                unit: this.newUnit || this.currentCounter.unit,
            });
            if (res === null)
                this.$router.push('/landing');
            this.$emit("close");
            this.fetchAllCounter();
        },
        ...mapActions(useUsersStore, {
            fetchAllCounter: "fetchAllCounter",
            setEditMode: "setEditMode"
        })
    },
    mounted() {
    },
}
</script> 

<style lang="scss">
.detailedSetting {
    input {
        font-size: 1rem;
        border-radius: 5px;

    }
}
</style>