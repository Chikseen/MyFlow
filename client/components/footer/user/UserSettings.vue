<template>
    <div>
        <div v-if="isLoginOk">
            <p>Welcome back {{ userData.name }}</p>
            <button @click="logout">Logout</button>
        </div>
        <div v-else>
            <p>Sign in with: </p>
            <ul>
                <li style="margin: 25px" @click="setIsUserChecked(true)"> <a :href="githubAuth">Github Login</a> </li>
                <li style="margin: 25px" @click="setIsUserChecked(true)"> <a :href="googleAuth">Google Login</a> </li>
            </ul>
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
            googlAuth: {
                clientID: "",
                redirectURI: "",
            }
        }
    },
    computed: {
        githubAuth() {
            return `https://github.com/login/oauth/authorize?client_id=${this.githubClientID}`
        },
        googleAuth() {
            return `https://accounts.google.com/o/oauth2/v2/auth?client_id=${this.googlAuth.clientID}&redirect_uri=${this.googlAuth.redirectURI}&response_type=code&scope=profile`
        },
        ...mapState(useUsersStore, {
            isLoginOk: 'isLoginOk',
            isUserChecked: 'isUserChecked',
            userData: 'userData',
        })
    },
    methods: {
        logout() {
            console.log("hi")
            document.cookie = "access_token= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            document.cookie = "auth_provider= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
            const usersStore = useUsersStore();
            usersStore.$reset();
            this.$router.push('/landing')
        },
        ...mapActions(useUsersStore, {
            setIsUserChecked: 'setIsUserChecked'
        }),
    },
    mounted() {
        const config = useRuntimeConfig();
        this.githubClientID = config.GITHUB_CLIENT_ID;
        this.googlAuth.clientID = config.GOOGLE_CLIENT_ID;
        this.googlAuth.redirectURI = config.GOOGLE_REDIRECT_URI;
    },

}
</script> 

<style lang="scss">

</style>