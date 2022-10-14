<template>
    <div class="chart" v-if="adjustedValues">
        <div class="chart_builder">
            <span class="chart_builder_yAxisBaseLine"></Span>
            <div class="chart_builder_yText">
                <p> units </p>
            </div>
            <div class="chart_builder_yAxisMarks">
                <p v-for="(item, index) in adjustedValues.marks.y" :key="index + 'a'"
                    class="chart_builder_yAxisMarks_text" :style="`top: ${item.offset}%;`">
                    {{item.value}}
                </p>
            </div>
        </div>
        <div class="chart_builder_content">
            <svg width="100%" height="100%" viewBox="0 0 100 100" preserveAspectRatio="none"
                xmlns="http://www.w3.org/2000/svg">
                <path :d="adjustedValues.path" stroke="#000000" stroke-width="0.5px" fill="#ffffff00" />
            </svg>
        </div>
        <div class="chart_builder">
            <div class="chart_builder_zero">
                <p> 0 </p>
            </div>
        </div>
        <div class="chart_builder">
            <span class="chart_builder_xAxisBaseLine"></span>
            <div class="chart_builder_xText">
                <div class="chart_builder_xAxisMarks">
                    <p v-for="(item, index) in adjustedValues.marks.x" :key="index + 'a'"
                        class="chart_builder_xAxisMarks_text" :style="`left: ${item.offset}%;`">
                        {{item.value}}
                    </p>
                </div>
                <p> Dates </p>
            </div>
        </div>
    </div>
</template>

<script>
import dates from "~~/assets/helper/dates"

export default {
    props: {
        values: { type: Object, default: null }
    },
    data() {
        return {}
    },
    methods: {},
    computed: {
        adjustedValues() {
            if (this.values?.length > 0) {
                // CALC BASE DATA
                let base = [];
                const timeOffset = new Date(new Date().setFullYear(new Date().getFullYear() - 1))

                this.values.forEach(value => {
                    base.push({
                        date: value.date,
                        value: value.value
                    })
                });

                base = base.filter(item => new Date(item.date) > timeOffset
                )

                // CALC MARKS
                //   Y
                const max = Math.max(...base.map(x => x.value))
                const min = Math.min(...base.map(x => x.value))

                let yMarks = [];
                base.forEach((value) => yMarks.push({
                    offset: 100 - Math.round(((value.value - min) / (max - min) * 100)),
                    value: value.value
                }));

                // CALC MARKS
                //   X
                let xMarks = [];
                xMarks.push({
                    offset: 0,
                    value: dates.ISOstringToDDMMYYYY(base[0].date)
                });
                xMarks.push({
                    offset: 100,
                    value: dates.ISOstringToDDMMYYYY(base[base.length - 1]?.date || base[0].date)
                });

                // CALC PATHING
                let path = `M 0 ${yMarks[0].offset || 0}`;
                if (yMarks.length > 1) {
                    const maxDate = Math.max(...base.map(x => new Date(x.date).getTime()));
                    const minDate = Math.min(...base.map(x => new Date(x.date).getTime()));
                    yMarks.forEach((mark, i) => {
                        if (i != 0) { // just to prevent "M0 100 L 0 100"
                            const date = new Date(base[i].date).getTime();
                            const step = 100 - (date - minDate) / (maxDate - minDate) * 100;
                            path += ` L ${100 - step} ${mark.offset}`
                        }
                    });
                }


                return {
                    main: base,
                    marks: {
                        y: yMarks,
                        x: xMarks,
                    },
                    path: path || "M 0 100L 12 47L 33 53L 100 0"
                }
            }
            return null
        },
    },
    mounted() { },
}
</script>

<style lang="scss">
.chart {
    display: grid;
    grid-template-columns: 50px calc(100% - 50px);
    grid-template-rows: calc(100% - 20px) 20px;
    height: 100%;
    width: 100%;


    &_builder {
        position: relative;

        //CONTENT
        &_content {
            position: relative;

            svg {
                position: absolute;
                top: 0;
                left: 0;
                height: 100%;
                width: 100%;
            }
        }

        //CONTENT

        //MARKS
        &_yAxisMarks {
            position: relative;
            height: calc(100% - 36px); // Additional offset for the height of the text

            &_text {
                position: absolute;
                top: 0;
                right: 0;
            }
        }

        &_xAxisMarks {
            position: relative;
            width: calc(100% - 100px); // Additional offset for the height of the text

            &_text {
                position: absolute;
                top: 0;
                right: 0;
                text-align: left !important;
            }
        }

        //MARKS

        // BASELINES
        &_yAxisBaseLine::after {
            content: "";
            position: absolute;
            top: 0;
            right: 0;
            width: 1px;
            height: 100%;
            background-color: #2d2d2d;
        }

        &_xAxisBaseLine::after {
            content: "";
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 1px;
            background-color: #2d2d2d;
        }

        &_zero {
            text-align: right;
        }

        // BASELINES

        p {
            margin: 0;
            padding: 3px;
            text-align: right;
            font-size: 0.7rem;
        }
    }
}
</style>