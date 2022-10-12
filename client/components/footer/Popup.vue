<template>
    <div class="settings" id="popup">
        <slot>
        </slot>
    </div>
</template>

<script>
import consolaGlobalInstance from 'consola';

export default {
    data() {
        return {
            startY: 0
        }
    },
    methods: {
        start(e) {
            console.log("s")
            this.startY = e.pageY || e.touches[0].pageY - this.popup.offsetTop;
        },
        move(e) {
            e.preventDefault();
            const currentOffset = e.pageY || e.touches[0].pageY - this.popup.offsetTop;
            const scrolledBy = currentOffset - this.startY;
            this.popup.style.height = `${540 - scrolledBy}px`;
        },
        end(e) {
            console.log(this.popup.offsetTop)
            if (this.popup.offsetTop <= 650)
                this.popup.style.height = "540px";
            else
                this.$emit("close");

        },
    },
    computed: {
        popup() {
            return document.getElementById("popup");
        }
    },
    mounted() {
        /* WORK ON IT LATER */
        /*         this.popup.addEventListener('mousedown', this.start);
                this.popup.addEventListener('touchstart', this.start);
        
                this.popup.addEventListener('mousemove', this.move);
                this.popup.addEventListener('touchmove', this.move);
        
                this.popup.addEventListener('mouseleave', this.end);
                this.popup.addEventListener('mouseup', this.end);
                this.popup.addEventListener('touchend', this.end); */
    },
}
</script>

<style lang="scss">
.settings {
    z-index: 6;
    position: absolute;
    bottom: 0;
    height: 520px;
    width: calc(100% - 20px);
    padding: 10px;
    background-color: #ffffffc5;
    backdrop-filter: blur(20px);
    box-shadow: 0px 0px 7px #2e2e2e7a;
    border-radius: 10px 10px 0 0;
    transition: all 0.1s;
}
</style>