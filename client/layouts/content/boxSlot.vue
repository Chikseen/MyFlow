<template>
    <div :class="['box', showColor]">
        <div v-if="data" class="box_counter">
            <p class="box_counter_name">{{data.name}}</p>
            <p class="box_counter_value">{{data.numbers.value}} {{data.numbers.unit}}</p>
            <div class="box_counter_date">
                <p class="box_counter_date_text">Last Entry</p>
                <span class="box_counter_date_detailed">
                    <p class="box_counter_date_detailed_text">{{dayDiff}} Days ago</p>
                </span>
            </div>
        </div>
        <slot v-else></slot>
    </div>
</template>

<script>
import dates from "~~/assets/helper/dates"

export default {
    props: {
        data: { type: Object, default: null }
    },
    computed: {
        dayDiff() {
            if (this.data)
                return dates.getTimeDiffInDays(this.data.numbers.date, null)
            else
                return null
        },
        showColor() {
            if (this.dayDiff) {
                if (this.dayDiff < 25)
                    return "box_green";
                else if (this.dayDiff < 90)
                    return "box_orange";
                else
                    return "box_red";
            } else
                return "box_default";
        }
    }
}
</script>

<style lang="scss">
.box {
    position: relative;
    height: 100%;
    width: 100%;
    font-size: 0.8rem;
    box-shadow: 0px 0px 7px #2e2e2e7a;
    border-radius: 5px;

    &_counter {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        padding: 20px;
        height: calc(100% - 40px);

        &_name {
            font-size: 1.1rem;
            font-weight: 800;
        }

        &_value {
            font-size: 1.3rem;
            font-weight: 500;
        }

        &_date {

            &_detailed {
                display: flex;
                flex-direction: column;
                justify-content: space-between;


                &_text {
                    text-align: left !important;
                    font-size: 0.7rem !important;
                    color: #e2e2e2 !important;
                }
            }

            &_text {
                font-size: 0.6rem;
                color: #bababa !important;
                text-align: left !important;
                font-weight: 100;
            }

        }
    }


    &_misc {
        padding: 20px;
        display: flex;
        justify-content: space-between;
        flex-direction: column;
        height: calc(100% - 40px);
    }

    &_green {
        background: linear-gradient(180deg, $boxGreenAkzt 0%, $boxGreen 70%, $boxGreen 90%);
    }

    &_orange {
        background: linear-gradient(180deg, $boxOrangeAkzt 0%, $boxOrange 70%, $boxOrange 90%);
    }

    &_red {
        background: linear-gradient(180deg, $boxRedAkzt 0%, $boxRed 70%, $boxRed 90%);
    }

    &_default {
        background: linear-gradient(180deg, $boxDefaultAkzt 0%, $boxDefault 70%, $boxDefault 90%);
    }

    p {
        text-align: center;
        padding-top: 0;
        margin: 0;
        color: #e2e2e2;
    }

    input {
        align-self: center;
        margin: 0 auto;
        padding: 5px;
        width: calc(100% - 25px);
        border-radius: 10px;
    }

    button {
        height: 30px;
        margin: 0 auto;
        background: none;
        font-size: 2.5rem;
    }
}
</style>