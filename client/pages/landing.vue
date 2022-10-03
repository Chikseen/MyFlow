<template>
    <div>
        <h1>MyFlow</h1>
        <p>Store all your monthly numbers</p>
        <NuxtLink v-if="isLoginOk" to="overview">OVERVIEW</NuxtLink>
    </div>
</template>

<script>
import { mapState } from 'pinia'
import { useUsersStore } from '~/store/users'

export default {
    computed: {
        ...mapState(useUsersStore, {
            isLoginOk: 'isLoginOk',
        })
    },
}
</script>