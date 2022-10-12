<template>
    <div class="detailed">
        <h3 @click="$router.push('/overview')">Overview > {{counterData?.name}}</h3>
        <div class="detailed_info_wrapper">
            <div class="detailed_info box-border">CHART
                {{counterData}}

            </div>
            <div class="detailed_info detailed_info_table_limiter box-border" ref="detailTableContentLimiter">
                <div class="detailed_info_table_add">
                    <input type="number" v-model="newValue">
                    <button @click="saveNewEntry">+</button>
                    <input type="date" :value="newDate || today" @change="setNewDate">
                </div>
                <div class="detailed_info detailed_info_table">
                    <div class="detailed_info_table_content" v-for="(number, index) in numbers" :key="index">
                        <p>
                            {{ number.value }}
                            <span class="detailed_info_table_content_unit">{{ number.unit }}</span>
                        </p>
                        <p>{{ convertDateFormat(number.date) }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import api from '~~/assets/helper/apiService';
import dates from '~~/assets/helper/dates';
import { mapState } from 'pinia'
import { useUsersStore } from '~/store/users'

import BoxWrapper from '~~/layouts/content/boxWrapper.vue';

export default {
    components: {
        BoxWrapper
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
        counterData() {
            return this.allCounter?.filter(item => item.id == this.counterId)[0]
        },
        counterId() {
            return this.$route.params.id
        },
        /* START HERE */
        /*    values() {
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
        */
        /* START HERE */
        // this should be ONE obj
        ...mapState(useUsersStore, {
            isEditMode: 'isEditMode',
            getAllCounterFromStore: 'getAllCounter',
            allCounter: "allCounter",
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
        async getDetailedNumbers() {
            const res = await api.get(`numbers/${this.counterId}`);
            if (res === null)
                this.$router.push('/landing');
            this.numbers = res;
            this.hideAddArea();
        },
        async saveNewEntry() {
            const dateToSend = new Date(this.newDate || this.today).toISOString()
            const res = await api.post(`numbers/${this.counterId}`, {
                id: this.counterId * 1,
                value: this.newValue,
                date: dateToSend,
                unit: "kWh"
            });
            if (res === null)
                this.$router.push('/landing');
            this.numbers.push(res);
            this.hideAddArea();
            this.newValue = "";
            this.newDate = this.today;
        },
        async removeNumber(id) {
            const res = await api.delete(`numbers/${this.counterId}`, { id: id });
            if (res === null)
                this.$router.push('/landing');
            if (res.status === 200)
                this.numbers = this.numbers.filter((numbers) => numbers.id !== id);
        },
    },
    async mounted() {
        await this.getAllCounterFromStore;
        this.getDetailedNumbers();
        this.hideAddArea();

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
                display: flex;
                justify-content: space-between;

                &_unit {
                    font-size: 0.7rem;
                    font-weight: lighter;
                    color: #7d7d7d;
                }

                p {
                    margin: 0;
                    padding: 10px 0;
                }
            }
        }
    }

    h3 {
        margin: 0;
        padding: 10px;
        font-size: 1.5rem;
        font-weight: 800;
    }
}
</style>