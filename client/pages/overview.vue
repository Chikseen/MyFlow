<template>
    <div class="overview">
        <h1>Counter</h1>
        <div class="overview_content_limiter" ref="overviewContentLimiter">
            <div class="overview_content_filter">
                <input type="text" v-model="searchCounter" placeholder="Search">
                <button>Sort</button>
            </div>
            <div class="overview_content">
                <boxWrapper>
                    <div v-for="item in allCounter" :key="item.id">
                        <boxSlot :data="item" @click="loadDetailed(item)" />
                    </div>
                    <boxSlot>
                        <h2>New Counter</h2>
                        <input type="text" v-model="createCoutnerText" @keyup.enter="createCounter">
                        <button @click="createCounter">+</button>
                    </boxSlot>
                </boxWrapper>
            </div>
        </div>
    </div>
</template>

<script>
import api from '~~/apiService';

import boxSlot from '~~/layouts/content/boxSlot.vue';
import boxWrapper from '~~/layouts/content/boxWrapper.vue';

export default {
    components: {
        boxSlot,
        boxWrapper,
    },
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
        handleScroll(event) {
            console.log("scroll", event)
        },
        async createCounter() {
            const res = await api.post("numbers", { name: this.createCoutnerText });
            if (res === null)
                this.$router.push('/landing')
            if (this.allCounter === null)
                this.allCounter = [];
            this.createCoutnerText = "";
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
        this.$refs.overviewContentLimiter.scrollTop = 40; // This sould be the height of the "Search and Filter" div
    },
}
</script>
    
<style lang="scss">
.overview {
    position: relative;
    height: 100vh;

    &_content {
        height: 100%;
        padding: 15px 0;
        scroll-snap-align: start;

        &_limiter {
            height: 80%;
            overflow: auto;
            scroll-snap-type: y mandatory;
        }

        &_filter {
            height: 30px;
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 10px 15px;
            scroll-snap-align: start;

            input {
                width: 75%;
                border: 0.1px rgb(135, 135, 135) solid;
                font-size: 1.3rem;
            }

            button {
                width: 25%;
            }
        }
    }
}
</style>

<!-- found no way for options api :thinking: -->
<!-- <script setup>
definePageMeta({
    middleware: 'auth'
})
</script>  -->
    