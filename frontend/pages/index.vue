<template>
  <div class="container">
    <h1 class="title">Code Global Premier League</h1>
    <AddTeam />
    <TeamTable />
  </div>
</template>

<script>
import { mapMutations } from 'vuex'
import * as signalR from '@microsoft/signalr'

export default {
  created() {
    this.MakeSignalRConnection()
    this.GetAllTeams()
  },
  methods: {
    ...mapMutations({
      add: 'team/add',
      update: 'team/update',
      delete: 'team/delete',
    }),
    async GetAllTeams() {
      const data = await this.$axios.$get('/api/team')
      data.forEach(this.add)
    },
    async MakeSignalRConnection() {
      const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${this.$config.baseURL}/teamhub`)
        .withAutomaticReconnect()
        .build()
      connection.on('create', this.add)
      connection.on('update', this.update)
      connection.on('delete', this.delete)
      await connection.start()
    },
  },
}
</script>

<style>
.container {
  margin: 0 auto;
  min-height: 100vh;
  min-width: 600px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.title {
  font-family: 'Quicksand', 'Source Sans Pro', -apple-system, BlinkMacSystemFont,
    'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
  display: block;
  font-weight: 300;
  font-size: 400%;
  color: #35495e;
  letter-spacing: 1px;
  padding-top: 20px;
}

.subtitle {
  font-weight: 300;
  font-size: 42px;
  color: #526488;
  word-spacing: 5px;
  padding-bottom: 15px;
}

.links {
  padding-top: 15px;
}

span {
  font-weight: bold;
}
</style>
