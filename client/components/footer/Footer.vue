<template>
    <div :class="['footer', {'footer_open':isSomethingOpen}]">
        <div class="footer_selection" @click="userExpanded = !userExpanded">
            <img v-if="userData.pictureURI?.length" :src="userData.pictureURI" alt="Logo" class="LogoBase">
            <UserIcon v-else class="IconBase" />
            <p>{{userData.name || "Login"}}</p>
        </div>
        <div class="footer_selection" v-if="isLoginOk" @click="setMode">
            <SettingIcon class="IconBase" />
            <p>Settings</p>
        </div>
        <div :class="isSomethingOpen ? 'footer_clickwrapper' : 'footer_clickwrapper_inactive'" @click.self="closeAll">
            <Transition name="footerSettings">
                <Popup v-if="userExpanded" @close="closeAll">
                    <UserSettings />
                </Popup>
                <Popup v-else-if="detailedSettingExpanded" @close="closeAll">
                    <DetailedSettings @close="closeAll" />
                </Popup>
            </Transition>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'

import UserSettings from "~~/components/footer/user/UserSettings.vue"
import DetailedSettings from "~~/components/footer/user/DetailedSettings.vue"
import Popup from "~~/components/footer/Popup.vue"
import UserIcon from "~~/assets/Icons/User-Icon.vue"
import SettingIcon from "~~/assets/Icons/Setting-Icon.vue"

export default {
    components: {
        UserIcon,
        SettingIcon,
        DetailedSettings,
        UserSettings,
        Popup,
    },
    data() {
        return {
            userExpanded: false,
            isEditEnabled: false,
            detailedSettingExpanded: false,
        }
    },
    methods: {
        closeAll() {
            this.userExpanded = false;
            this.detailedSettingExpanded = false;
        },
        setMode() {
            if (this.$route.name == "overview") {
                this.isEditEnabled = !this.isEditEnabled;
                this.setEditMode(this.isEditEnabled);
            }
            if (this.$route.name == "detailed-id")
                this.detailedSettingExpanded = !this.detailedSettingExpanded;
        },
        ...mapActions(useUsersStore, {
            setEditMode: 'setEditMode'
        }),
    },
    computed: {
        isSomethingOpen() {
            const sthExpanded = this.userExpanded || this.detailedSettingExpanded
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
    background-color: #fafafa;
    border-radius: 10px 10px 0 0;

    &_clickwrapper {
        position: fixed;
        top: 0;
        height: 100%;
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
        gap: 15px;
    }

    p {
        margin: 9px 0;
    }

    svg {
        height: 40px;
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