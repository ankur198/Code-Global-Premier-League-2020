using System;
using System.Collections.Generic;
using backend.Hubs;
using backend.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;

namespace Services
{
    public class TeamService
    {
        private enum MethodName { create, update, delete }

        private readonly IMongoCollection<Team> teams;
        private readonly IHubContext<TeamHub> hubContext;

        public TeamService(IDatabaseSettings settings, IHubContext<TeamHub> hubContext)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            teams = database.GetCollection<Team>(settings.CollectionName);
            this.hubContext = hubContext;
        }

        public List<Team> Get() =>
            teams.Find(x => true).ToList();

        public Team Get(string id) =>
            teams.Find(x => x.Id == id).FirstOrDefault();

        public Team Create(Team team)
        {
            teams.InsertOne(team);
            SendNotificationToHub(MethodName.create, team);
            return team;
        }

        private void Update(Team team)
        {
            teams.ReplaceOne(teamDb => teamDb.Id == team.Id, team);
            SendNotificationToHub(MethodName.update, team);
        }

        public void Remove(Team team) =>
            Remove(team.TeamName);


        public void Remove(string id)
        {
            var team = Get(id);
            teams.DeleteOne(teamDb => teamDb.Id == id);
            SendNotificationToHub(MethodName.delete, team);
        }

        private async void SendNotificationToHub(MethodName methodName, Team team)
        {
            var name = Enum.GetName<MethodName>(methodName);
            await hubContext.Clients.All.SendAsync(name, team);
        }

        public Team AssignWin(string id)
        {
            var team = Get(id);
            team.Wins += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }

        public Team AssignTie(string id)
        {
            var team = Get(id);
            team.Ties += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }

        public Team AssignLoss(string id)
        {
            var team = Get(id);
            team.Losses += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }
    }
}