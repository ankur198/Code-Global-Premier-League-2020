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

        public Team Get(string name) =>
            teams.Find(x => x.TeamName == name).FirstOrDefault();

        public Team Create(Team team)
        {
            teams.InsertOne(team);
            SendNotificationToHub(MethodName.create, team);
            return team;
        }

        private void Update(Team team)
        {
            teams.ReplaceOne(teamDb => teamDb.TeamName == team.TeamName, team);
            SendNotificationToHub(MethodName.update, team);
        }

        public void Remove(Team team) =>
            Remove(team.TeamName);


        public void Remove(string teamName)
        {
            var team = Get(teamName);
            teams.DeleteOne(teamDb => teamDb.TeamName == teamName);
            SendNotificationToHub(MethodName.delete, team);
        }

        private async void SendNotificationToHub(MethodName methodName, Team team)
        {
            await hubContext.Clients.All.SendAsync(nameof(methodName), team);
        }

        public Team AssignWin(string teamName)
        {
            var team = Get(teamName);
            team.Wins += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }

        public Team AssignTie(string teamName)
        {
            var team = Get(teamName);
            team.Ties += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }

        public Team AssignLoss(string teamName)
        {
            var team = Get(teamName);
            team.Losses += 1;
            team.UpdateScore();
            Update(team);
            return team;
        }
    }
}