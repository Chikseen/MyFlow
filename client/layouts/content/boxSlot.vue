<template>
    <div class="box">
        <div v-if="data">
            <h2>{{data.name}}</h2>
        </div>
        <div v-else>
            <slot></slot>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        data: { type: Object, default: null }
    }
}
</script>

<style lang="scss">
.box {
    position: relative;
    height: 100%;
    width: 100%;
    font-size: 0.8rem;
    background: linear-gradient(180deg, #56d95403 0%, #56d95403 70%, #56d954 75%);
    box-shadow: 0px 0px 5px #00000040;
    border-radius: 5px;


    h2 {
        text-align: center;
        margin-top: 5px;
    }

    input {
        margin: 15px auto;
        padding: 5px;
    }

    button {
        height: 50px;
        width: 50px;
        margin: 10px auto;
        background: none;
        font-size: 3rem;
    }
}
</style>