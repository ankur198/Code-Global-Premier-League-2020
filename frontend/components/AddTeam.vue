<template>
  <div>
    <form @submit.prevent="addTeam">
      <input v-model="name" type="text" />
      <input type="submit" value="Add" />
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      name: '',
    }
  },
  methods: {
    async addTeam() {
      await this.$axios.$post('api/team', { team_name: this.name })
      this.name = ''
    },
  },
}
</script>

<style lang="scss" scoped>
div {
  padding-top: 50px;
  input {
    padding: 10px 20px;
  }
}
</style>
