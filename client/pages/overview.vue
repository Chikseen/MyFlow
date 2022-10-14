<template>
    <div class="overview">
        <h1>MyFlow</h1>
        <div class="overview_content_limiter" ref="overviewContentLimiter">
            <div class="overview_content_filter">
                <input type="text" v-model="searchCounter" placeholder="Search">
                <button>Sort</button>
            </div>
            <div class="overview_content">
                <boxWrapper>
                    <div v-for="item in allCounter" :key="item.id"
                        :class="['overview_content_box', {'overview_content_box_shake':isEditMode}]"
                        :style="`animation-delay: ${Math.floor(Math.random() * 200)}ms;`">
                        <span v-if="isEditMode" class="overview_content_box_remove"
                            @click="removeCounter(item.id)">X</span>
                        <boxSlot :data="item" @click="loadDetailed(item)" />
                    </div>
                    <div class="overview_content_box">
                        <boxSlot class="box_misc">
                            <p>New Counter</p>
                            <input type="text" v-model="createCoutnerText" @keyup.enter="createCounter">
                            <button @click="createCounter">+</button>
                        </boxSlot>
                    </div>
                </boxWrapper>
            </div>
        </div>
    </div>
</template>

<script>
import api from '~~/assets/helper/apiService';
import { mapState, mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'

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
            allCounter: null,
            searchCounter: "",
        }
    },
    methods: {
        loadDetailed(counter) {
            if (!this.isEditMode)
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
            this.setAllCounter(this.allCounter)
        },
        async removeCounter(id) {
            if (this.isEditMode) {
                const res = await api.delete("numbers", { id: id });
                if (res === null)
                    this.$router.push('/landing')
                if (res.status === 200)
                    this.allCounter = this.allCounter.filter((counter) => counter.id !== id)
                this.setAllCounter(this.allCounter)
            }
        },
        ...mapActions(useUsersStore, {
            setAllCounter: 'setAllCounter',
        }),
    },
    computed: {
        ...mapState(useUsersStore, {
            isEditMode: 'isEditMode',
            getAllCounterFromStore: 'getAllCounter',
        })
    },
    async mounted() {
        this.allCounter = await this.getAllCounterFromStore;
        this.$refs.overviewContentLimiter.scrollTo({ top: 40, behavior: 'smooth' });
    },
}
</script>
    
<style lang="scss">
.overview {
    position: relative;
    height: 100%;
    padding: 0 15px;

    &_content {
        height: calc(100% - 10px);
        margin: 5px;

        &_limiter {
            z-index: 0;
            height: calc(100% - 80px) !important;
            overflow: auto;
            scroll-snap-type: y mandatory;
        }

        &_box {
            position: relative;
            scroll-snap-align: start;
            padding: 5px;

            &_remove {
                position: absolute;
                top: -10px;
                right: -10px;
                width: 20px;
                height: 20px;
                background-color: red;
                border-radius: 5px;
                text-align: center;
                vertical-align: center;
                z-index: 8;
            }

            &_shake {
                animation: shake .2s infinite alternate linear;
            }
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
                font-size: 1.1rem;
            }

            button {
                width: 25%;
            }
        }
    }
}

@keyframes shake {
    0% {
        transform: rotateZ(0deg);
    }

    0% {
        transform: rotateZ(2deg);
    }

    100% {
        transform: rotateZ(-2deg);
    }
}
</style>

<!-- found no way for options api :thinking: -->
<!-- <script setup>
definePageMeta({
    middleware: 'auth'
})
</script>  -->
    