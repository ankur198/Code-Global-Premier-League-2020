export const state = () => ({
  teams: [],
})

export const mutations = {
  add(state, team) {
    state.teams.push(team)
  },
  update(state, team) {
    const oldTeam = state.teams.filter((x) => x.team_name === team.team_name)[0]
    const index = state.teams.indexOf(oldTeam)
    state.teams.splice(index, 1, team)
  },
  delete(state, team) {
    state.teams.splice(state.teams.indexOf(team), 1)
  },
}
