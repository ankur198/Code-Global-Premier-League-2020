using System.Collections.Generic;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService teamService;

        public TeamController(TeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        public ActionResult<List<Team>> Get() =>
            teamService.Get();

        [HttpGet("{name}", Name = "GetTeam")]
        public ActionResult<Team> Get(string name) =>
            teamService.Get(name);

        [HttpPost]
        public ActionResult<Team> Create(Team team)
        {
            teamService.Create(team);
            return Created(team.TeamName, team);
        }

        [HttpGet("win/{teamName}")]
        public ActionResult<Team> AssignWin(string teamName)
        {
            var team = teamService.AssignWin(teamName);
            return Ok(team);
        }

        [HttpGet("loss/{teamName}")]
        public ActionResult<Team> AssignLoss(string teamName)
        {
            var team = teamService.AssignLoss(teamName);
            return Ok(team);
        }

        [HttpGet("tie/{teamName}")]
        public ActionResult<Team> AssignTie(string teamName)
        {
            var team = teamService.AssignTie(teamName);
            return Ok(team);
        }
    }
}