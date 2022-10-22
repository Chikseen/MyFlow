<template>
    <div class="detailed">
        <h3 @click="$router.push('/overview')">&lt;- Overview: {{currentCounter?.name}}</h3>
        <div class="detailed_info_wrapper">
            <div class="detailed_info detailed_info_chart box-border">
                <Chart :values="numbers" />
            </div>
            <div class="detailed_info detailed_info_table_limiter box-border" ref="detailTableContentLimiter">
                <div class="detailed_info_table_add">
                    <input type="number" v-model="newValue">
                    <button @click="saveNewEntry">+</button>
                    <input type="date" :value="newDate || today" @change="setNewDate">
                </div>
                <div class="detailed_info detailed_info_table">
                    <TransitionGroup name="values" tag="div">
                        <div class="detailed_info_table_content" v-for="(number, index) in valuesForTable" :key="index">
                            <p>
                                {{ number.value }}
                                <span class="detailed_info_table_content_unit">{{ currentCounter.unit }}</span>
                            </p>
                            <p>{{ convertDateFormat(number.date) }}</p>
                            <span v-if="isEditMode" class="detailed_info_table_content_remove"
                                @click="removeNumber(number.id)">X</span>
                        </div>
                    </TransitionGroup>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import api from '~~/assets/helper/apiService';
import dates from '~~/assets/helper/dates';
import { mapState, mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'

import BoxWrapper from '~~/layouts/content/boxWrapper.vue';
import Chart from "~~/components/chart/ChartWrapper.vue"

export default {
    components: {
        BoxWrapper,
        Chart,
    },
    name: "detailed",
    data() {
        return {
            numbers: null,
            newValue: "",
            newDate: null,
        }
    },
    computed: {
        today() {
            return new Date().toISOString().split('T')[0];
        },
        values() {
            let values = {};
            return values;
        },
        counterId() {
            return this.$route.params.id
        },
        valuesForTable() {
            if (this.numbers) {
                const numbers = [...this.numbers]
                return numbers.reverse();
            }
            return null
        },
        ...mapState(useUsersStore, {
            isEditMode: 'isEditMode',
            getAllCounterFromStore: 'getAllCounter',
            allCounter: "allCounter",
            currentCounter: "curentCounter",
        })
    },
    methods: {
        convertDateFormat(date) {
            return dates.ISOstringToDDMMYYYY(date);
        },
        setNewDate(e) {
            this.newDate = e.target.value;
        },
        hideAddArea() {
            this.$refs.detailTableContentLimiter.scrollTo({ top: 61, behavior: 'smooth' });
        },
        sortNumbers() {
            // is meant for diffrent sort -> Sort by (created, date, value)
            this.numbers.sort((a, b) => new Date(a.date).getTime() > new Date(b.date).getTime() ? 1 : -1)
        },
        async getDetailedNumbers() {
            const res = await api.get(`numbers/${this.counterId}`);
            if (res === null)
                this.$router.push('/landing');
            this.numbers = res;
            this.sortNumbers();
            this.hideAddArea();
        },
        async saveNewEntry() {
            const dateToSend = new Date(this.newDate || this.today).toISOString()
            const res = await api.post(`numbers/${this.counterId}`, {
                id: this.counterId * 1,
                value: this.newValue,
                date: dateToSend,
            });
            if (res === null)
                this.$router.push('/landing');
            this.numbers.push(res);
            this.hideAddArea();
            this.newValue = "";
            this.newDate = this.today;
            this.sortNumbers();
        },
        async removeNumber(id) {
            const res = await api.delete(`numbers/${this.counterId}`, { id: id });
            if (res === null)
                this.$router.push('/landing');
            if (res.status === 200) {
                this.numbers = this.numbers.filter((numbers) => numbers.id !== id);
                this.setEditMode(false)
            }
            this.sortNumbers();
        },
        ...mapActions(useUsersStore, {
            setCurrentCounter: 'setCurrentCounter',
            setEditMode: "setEditMode"
        }),
    },
    async mounted() {
        await this.getAllCounterFromStore;
        this.getDetailedNumbers();
        this.hideAddArea();
        this.setCurrentCounter();
    },
}
</script>

<style lang="scss">
.detailed {
    position: relative;
    padding: 0 15px;
    height: 100%;

    &_info {
        grid-column: span 2;
        grid-row: span 2;

        &_wrapper {
            display: grid;
            grid-template-rows: 20% 20% 20% 20%;
            height: calc(100% - 137px);
            padding: 10px;
            gap: 15px;
        }

        &_table {
            padding: 0 20px;
            min-height: calc(100% + 61px);
            scroll-snap-align: start;

            &_limiter {
                overflow: auto;
                scroll-snap-type: y mandatory;
            }

            &_add {
                padding: 0 20px;
                scroll-snap-align: start;
                display: grid;
                grid-template-columns: 75% 25%;

                input {
                    margin: 5px;
                    font-size: 1rem;
                }

                button {
                    grid-row: span 2;
                    border-radius: 7px;
                    background-color: #ffffff00;
                    font-size: 2rem;
                }
            }

            &_content {
                position: relative;
                display: flex;
                justify-content: space-between;

                &_unit {
                    font-size: 0.7rem;
                    font-weight: lighter;
                    color: #7d7d7d;
                }

                &_remove {
                    position: absolute;
                    top: 7px;
                    right: 0;
                    width: 20px;
                    height: 20px;
                    background-color: red;
                    border-radius: 5px;
                    text-align: center;
                    vertical-align: center;
                }

                p {
                    margin: 0;
                    padding: 10px 0;
                }
            }
        }

        &_chart {
            position: relative;
            width: 100%;
            height: 100%;
        }
    }


    h3 {
        margin: 0;
        padding: 10px;
        font-size: 1.5rem;
        font-weight: 800;
    }
}

// VALUES TRANSITION
.values-move,
.values-enter-active,
.values-leave-active {
    transition: all 0.5s ease;
}

.values-enter-from,
.values-leave-to {
    opacity: 0;
    transform: translateY(100%);
}

.values-leave-active {
    position: absolute;
}
</style>