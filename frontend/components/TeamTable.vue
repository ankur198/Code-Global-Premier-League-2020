<template>
  <div class="table">
    <vue-good-table
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
    />
  </div>
</template>

<script>
export default {
  computed: {
    teams() {
      return this.$store.state.team.teams
    },
  },
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
    }
  },
}
</script>

<style lang="scss" scoped>
.table {
  padding: 20px;
  width: 100%;
  max-width: 1000px;

  .row {
    display: grid;
    grid-template-columns: 2fr 1fr 1fr 1fr 1fr;
    border: 2px solid rgb(110, 110, 110);

    * {
      padding: 10px;
      border: 2px solid rgb(110, 110, 110);
    }
  }

  .header {
    font-size: 150%;
  }
}
</style>
