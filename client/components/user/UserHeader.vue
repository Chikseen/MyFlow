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
                        <li @click="setUserCheckState"> <a :href="githubAuth">Github Login</a> </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
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
        isLoginOk() {
            const usersStore = useUsersStore();
            return usersStore.isLoginOk;
        },
        isUserChecked() {
            const usersStore = useUsersStore();
            return usersStore.isUserChecked;
        },
        userData() {
            const usersStore = useUsersStore();
            return usersStore.userData;
        }
    },
    methods: {
        setUserCheckState() {
            const usersStore = useUsersStore();
            usersStore.setIsUserChecked(true);
        },
        logout() {
            document.cookie = "access_token= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            document.cookie = "auth_provider= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            const usersStore = useUsersStore();
            usersStore.setIsUserChecked(false);
            usersStore.setIsLoginOk(false);
        }
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