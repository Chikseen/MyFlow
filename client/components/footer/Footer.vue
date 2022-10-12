<template>
    <div :class="['footer', {'footer_open':isSomethingOpen}]">
        <div class="footer_selection" @click="userExpanded = !userExpanded">
            <UserIcon class="IconBase" />
            <p>{{userData.name || "Login"}}</p>
        </div>
        <div class="footer_selection" v-if="isLoginOk" @click="setMode">
            <SettingIcon class="IconBase" />
            <p>Settings</p>
        </div>
        <div :class="isSomethingOpen ? 'footer_clickwrapper' : 'footer_clickwrapper_inactive'" @click="closeAll">
            <Transition name="footerSettings">
                <Popup v-if="userExpanded" @close="closeAll">
                    <UserSettings />
                </Popup>
            </Transition>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'

import UserSettings from "~~/components/footer/user/UserSettings.vue"
import Popup from "~~/components/footer/Popup.vue"
import UserIcon from "~~/assets/Icons/User-Icon.vue"
import SettingIcon from "~~/assets/Icons/Setting-Icon.vue"

export default {
    components: {
        UserIcon,
        SettingIcon,
        UserSettings,
        Popup,
    },
    data() {
        return {
            userExpanded: false,
            isEditEnabled: false,
        }
    },
    methods: {
        closeAll() {
            this.userExpanded = false;
        },
        setMode() {
            this.isEditEnabled = !this.isEditEnabled;
            this.setEditMode(this.isEditEnabled);
        },
        ...mapActions(useUsersStore, {
            setEditMode: 'setEditMode'
        }),
    },
    computed: {
        isSomethingOpen() {
            const sthExpanded = this.userExpanded
            this.$emit("sthExpanded", sthExpanded)
            return sthExpanded;
        },
        ...mapState(useUsersStore, {
            userData: 'userData',
            isLoginOk: 'isLoginOk',
        })
    },
}
</script>  
    
<style lang="scss">
.footer {
    z-index: 4;
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    height: 40px;
    padding: 10px 0;
    display: flex;
    justify-content: space-evenly;
    box-shadow: 0px 0px 7px #2e2e2e7a;
    border-radius: 10px 10px 0 0;

    &_clickwrapper {
        position: fixed;
        top: 0;
        height: 100vh;
        left: 0;
        width: 100vw;
        background-color: #96969630;
        z-index: 5;

        &_inactive {
            position: fixed;
            bottom: 0;
            height: 0;
            left: 0;
            width: 100vw;
            z-index: 0;
        }
    }

    &_selection {
        display: flex;
    }

    p {
        margin: 9px 0;
    }
}

// Animations
.footerSettings-enter-active,
.footerSettings-leave-active {
    transition: all 0.3s ease;
}

.footerSettings-enter-from,
.footerSettings-leave-to {
    height: 0;
}
</style>