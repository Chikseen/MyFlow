<template>
    <div>
        <a :href="githubAuth">Github Login</a>
    </div>
</template>

<script>
import api from '~~/apiService'

export default {
    data() {
        return {
            githubClientID: ""
        }
    },
    computed: {
        githubAuth() {
            return `https://github.com/login/oauth/authorize?client_id=${this.githubClientID}`
        }
    },
    mounted() {
        const config = useRuntimeConfig()
        this.githubClientID = config.GITHUB_CLIENT_ID
    },
}
</script>

