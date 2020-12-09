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
            try
            {
                teamService.Create(team);
                return Created(team.Id, team);
            }
            catch (System.Exception)
            {
                return BadRequest(team);
            }
        }

        [HttpGet("win/{id}")]
        public ActionResult<Team> AssignWin(string id)
        {
            var team = teamService.AssignWin(id);
            return Ok(team);
        }

        [HttpGet("loss/{id}")]
        public ActionResult<Team> AssignLoss(string id)
        {
            var team = teamService.AssignLoss(id);
            return Ok(team);
        }

        [HttpGet("tie/{id}")]
        public ActionResult<Team> AssignTie(string id)
        {
            var team = teamService.AssignTie(id);
            return Ok(team);
        }
    }
}