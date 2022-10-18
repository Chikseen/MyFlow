<template>
    <div class="overview">
        <h1>MyFlow</h1>
        <div class="overview_content_limiter" ref="overviewContentLimiter">
            <div :class="['overview_content_filter', {'overview_content_filter_expanded': isSortExpanded}]">
                <input type="text" v-model="searchCounter" placeholder="Search" @focus="isSortExpanded = false">
                <button @click="isSortExpanded = true">
                    <Transition name="filterButtons" mode="out-in">
                        <div v-if="isSortExpanded" class="overview_content_filter_expanded_buttons">
                            <button @click="sortBy = 'date'">Date</button>
                            <button @click="sortBy = 'value'">Value</button>
                            <button @click="sortBy = 'name'">Name</button>
                        </div>
                        <p v-else>Sort</p>
                    </Transition>
                </button>
            </div>
            <div class="overview_content">
                <boxWrapper>
                    <TransitionGroup name="overview" tag="div">
                        <div v-for="item in filterdCounter" :key="item.id"
                            :class="['overview_content_box', {'overview_content_box_shake':isEditMode}]"
                            :style="`animation-delay: ${Math.floor(Math.random() * 200)}ms;`">
                            <span v-if="isEditMode" class="overview_content_box_remove"
                                @click="removeCounter(item.id)">X</span>
                            <boxSlot :data="item" @click="loadDetailed(item)" style="position: relative;" />
                        </div>
                        <div class="overview_content_box" key="overviewAdd">
                            <boxSlot class="box_misc">
                                <p>New Counter</p>
                                <input type="text" v-model="createCoutnerText" @keyup.enter="createCounter">
                                <button @click="createCounter">+</button>
                            </boxSlot>
                        </div>
                    </TransitionGroup>
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
            searchCounter: " ",
            isSortExpanded: false,
            sortBy: "date"
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
                if (res.status === 200) {
                    this.setEditMode(false);
                    this.allCounter = this.allCounter.filter((counter) => counter.id !== id)
                }
                this.setAllCounter(this.allCounter)
            }
        },
        ...mapActions(useUsersStore, {
            setAllCounter: 'setAllCounter',
            setEditMode: "setEditMode"
        }),
    },
    computed: {
        filterdCounter() {
            let filtered = this.allCounter
            if (filtered) {
                if (this.searchCounter != "")
                    filtered = this.allCounter.filter((item) => (item.name).toLowerCase().match((this.searchCounter.trim()).toLowerCase()))

                if (this.sortBy === "date")
                    filtered.sort((a, b) => new Date(a.numbers.date).getTime() < new Date(b.numbers.date).getTime() ? 1 : -1)
                else if (this.sortBy === "name")
                    filtered.sort((a, b) => (a.name).toLowerCase() > (b.name).toLowerCase() ? 1 : -1)
                else
                    filtered.sort((a, b) => a.numbers.value - b.numbers.value)

                return filtered
            }
            return null


        },
        ...mapState(useUsersStore, {
            isEditMode: 'isEditMode',
            getAllCounterFromStore: 'getAllCounter',
        })
    },
    async mounted() {
        this.allCounter = await this.getAllCounterFromStore;
        this.$refs.overviewContentLimiter.scrollTo({ top: 40, behavior: 'smooth' });
        this.setEditMode(false)
    },
    watch: {
        searchCounter(e) {
            if (e == "") // empty string is not allowed because is destroyed the animations for whatever reason
                this.searchCounter = " ";
        }
    }
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
                //animation: shake .2s infinite alternate linear;
            }
        }

        &_filter {
            height: 30px;
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 10px 15px;
            scroll-snap-align: start;

            &_expanded {
                &_buttons {
                    display: flex;

                    button {
                        height: 100%;
                    }
                }

                input {
                    flex-basis: 5% !important;
                }

                button {
                    flex-basis: 95% !important;
                }
            }

            input {
                width: 100%;
                border: 0.1px rgb(135, 135, 135) solid;
                font-size: 1.1rem;
                flex-basis: 75%;
                transition: all 0.5s;
            }

            button {
                width: 100%;
                flex-basis: 25%;
                transition: all 0.5s;
            }

            p {
                margin: auto;
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

// FILTER
.filterButtons-enter-active,
.filterButtons-leave-active {
    transition: opacity 0.25s ease;
}

.filterButtons-enter-from,
.filterButtons-leave-to {
    opacity: 0;
}

// OVERVIEW
.overview-move,
.overview-enter-active,
.overview-leave-active {
    transition: all 0.5s ease;
}

.overview-enter-from {
    opacity: 0;
    transform: translateX(100%);
}

.overview-leave-to {
    opacity: 0;
    transform: translateX(-100%);
}

.overview-leave-active {
    position: absolute;
}
</style>

<!-- found no way for options api :thinking: -->
<!-- <script setup>
definePageMeta({
    middleware: 'auth'
})
</script>  -->