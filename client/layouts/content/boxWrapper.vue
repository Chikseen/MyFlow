<template>
    <div class="box_wrapper">
        <slot></slot>
    </div>
</template>

<style lang="scss">
.box_wrapper {
    position: relative;
    display: grid;
    grid-template-columns: repeat(auto-fit, calc(50% - 7.5px));
    //aspect-ratio: 1 / 1.5125;
    aspect-ratio: 1 / 1;
    width: calc(100% - 25px);
    grid-auto-rows: calc(50% - 7.5px);
    gap: 10px;
    margin: 0 15px;
    padding-top: 15px;
}
</style>