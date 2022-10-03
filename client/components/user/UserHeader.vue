<template>
    <div :class="['userHeader_wrapper', {'userHeader_wrapper_expanded': isUserScreenExpanded && !isUserChecked}]">
        <div v-if="isUserChecked">
            <p>LOADING</p>
        </div>
        <div v-else class="userHeader_screen">
            <div v-if="isLoginOk" @click="isUserScreenExpanded = !isUserScreenExpanded">
                <p>{{ userData?.name }}</p>
            </div>
            <div v-else @click="isUserScreenExpanded = !isUserScreenExpanded">
                <p> Start now </p>
            </div>
            <div v-if="isUserScreenExpanded">
                <div v-if="isLoginOk">
                    <p>Welcome back</p>
                    <button @click="logout">Logout</button>
                </div>
                <div v-else>
                    <p>Sign in with: </p>
                    <ul>
                        <li @click="setIsUserChecked(true)"> <a :href="githubAuth">Github Login</a> </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'pinia'
import { useUsersStore } from '~/store/users'

export default {
    data() {
        return {
            githubClientID: "",
            isUserScreenExpanded: false,
        }
    },
    computed: {
        githubAuth() {
            return `https://github.com/login/oauth/authorize?client_id=${this.githubClientID}`
        },
        ...mapState(useUsersStore, {
            isLoginOk: 'isLoginOk',
            isUserChecked: 'isUserChecked',
            userData: 'userData',
        })
    },
    methods: {
        logout() {
            document.cookie = "access_token= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            document.cookie = "auth_provider= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            const usersStore = useUsersStore();
            usersStore.$reset();
        },
        ...mapActions(useUsersStore, {
            setIsUserChecked: 'setIsUserChecked'
        }),
    },
    mounted() {
        const config = useRuntimeConfig();
        this.githubClientID = config.GITHUB_CLIENT_ID;
    },

}
</script>  
    
<style lang="scss">
.userHeader {
    &_wrapper {
        position: absolute;
        top: 0;
        right: 0;
        width: 75px;
        background-color: aqua;

        &_expanded {
            width: 200px;
        }
    }
}
</style>