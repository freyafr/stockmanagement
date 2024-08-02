<template>
  <!--<img alt="Vue logo" src="./assets/logo.png">-->
  <AccountInformation ref="accountInfo"/>
  <PurchaseList @stock-added="reloadAccountInformation"/>
</template>

<script>
import AccountInformation from './components/AccountInformation.vue'
import PurchaseList from './components/PurchaseList.vue'

export default {
  name: 'App',
  components: {
    AccountInformation,
    PurchaseList
  },
  methods: {
    async reloadAccountInformation() {
      await this.$refs.accountInfo.fetchUserAccount();
    },
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;
  color: #2c3e50;
  margin-top: 20px;
}
</style>
