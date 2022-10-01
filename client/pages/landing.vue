<template>
    <div>
        <p>isLoginOk: {{ isLoginOk }}</p>
        <p>isUserChecked: {{ isUserChecked }}</p>
        <div v-if="!isLoginOk && !isUserChecked">
            <a :href="githubAuth">Github Login</a>
        </div>
        <div v-else>
            <p>Welcome back {{ userData?.name }}</p>
        </div>
    </div>
</template>

<script>
import { useUsersStore } from '~/store/users'

export default {
    data() {
        return {
            githubClientID: ""
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
    methods: {},
    mounted() {
        const config = useRuntimeConfig();
        this.githubClientID = config.GITHUB_CLIENT_ID;
    },

}
</script>  