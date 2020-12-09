<template>
  <div class="table">
    <vue-good-table
      v-show="selectedTeams.length < 2"
      :columns="columns"
      :rows="teams"
      :pagination-options="{
        enabled: true,
      }"
      :sort-options="{
        enabled: true,
        initialSortBy: { field: 'score', type: 'desc' },
      }"
      :search-options="{
        enabled: true,
      }"
      @on-row-click="onRowClick"
    />

    <dir v-if="selectedTeams.length > 1" class="popup">
      <div class="info">
        <div>
          {{ selectedTeams[0].team_name }}
        </div>
        <span>V/S</span>
        <div>
          {{ selectedTeams[1].team_name }}
        </div>
      </div>
      <div class="actions">
        <button @click.prevent="sendFirstTeamWon">
          {{ selectedTeams[0].team_name }} Won
        </button>
        <button @click.prevent="sendTie">Tie</button>
        <button @click.prevent="sendSecondTeamWon">
          {{ selectedTeams[1].team_name }} Won
        </button>
        <button @click.prevent="resetSelectedTeams">Cancel</button>
      </div>
    </dir>
  </div>
</template>

<script>
export default {
  data() {
    return {
      columns: [
        {
          label: 'Team Name',
          field: 'team_name',
        },
        {
          label: 'Score',
          field: 'score',
          type: 'number',
        },
        {
          label: 'Wins',
          field: 'wins',
          type: 'number',
        },
        {
          label: 'Losses',
          field: 'losses',
          type: 'number',
        },
        {
          label: 'Ties',
          field: 'ties',
          type: 'number',
        },
      ],
      selectedTeams: [],
    }
  },
  computed: {
    teams() {
      return this.$store.state.team.teams
    },
  },
  methods: {
    onRowClick(params) {
      this.selectedTeams.push(params.row)
    },
    resetSelectedTeams() {
      this.selectedTeams = []
    },
    async sendFirstTeamWon() {
      const teamWon = this.selectedTeams[0]
      const teamLost = this.selectedTeams[1]
      await this.sendWonLost(teamWon, teamLost)
      this.resetSelectedTeams()
    },
    async sendSecondTeamWon() {
      const teamWon = this.selectedTeams[1]
      const teamLost = this.selectedTeams[0]
      await this.sendWonLost(teamWon, teamLost)
      this.resetSelectedTeams()
    },
    async sendWonLost(teamWon, teamLost) {
      await this.$axios.$get(`api/team/win/${teamWon.id}`)
      await this.$axios.$get(`api/team/loss/${teamLost.id}`)
    },
    async sendTie() {
      await this.$axios.$get(`api/team/tie/${this.selectedTeams[0].id}`)
      await this.$axios.$get(`api/team/tie/${this.selectedTeams[1].id}`)
      this.resetSelectedTeams()
    },
  },
}
</script>

<style lang="scss" scoped>
.table {
  padding: 20px;
  width: 100%;
  max-width: 1000px;
  position: relative;
}
</style>
