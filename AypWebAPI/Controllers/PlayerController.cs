using AypWebAPI.Context;
using AypWebAPI.Models.Dto;
using AypWebAPI.Models.Entities;
using AypWebAPI.Models.Exceptions;
using AypWebAPI.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace AypWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ILogger _logger;

        public PlayerController(ApplicationContext context, ILogger<PlayerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult PlayerFromRoot([FromRoute] int id)
        {
            var data = _context.Players;
            var result = data.Where(player => player.Id == id).SingleOrDefault();
            if (result is null)
            {
                throw new AppException($"There is no player with id {id}");
            }
            PlayerDto playerDto = new PlayerDto()
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                BackNumber = result.BackNumber,
                TeamId = result.TeamId, 
            };
            return Ok(playerDto);
        }

        [HttpGet("id")]
        public IActionResult PlayerFromQuery([FromQuery] int id)
        {
            var data = _context.Players;
            var result = data.Where(player => player.Id == id).SingleOrDefault();
            if (result is null)
            {
                throw new AppException($"There is no player with id {id}");
            }
            PlayerDto playerDto = new PlayerDto()
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                BackNumber = result.BackNumber,
                TeamId = result.TeamId,
            };
            return Ok(playerDto);
        }

        [HttpGet]
        public IActionResult Player([FromQuery] GetPlayersWithQuery getPlayersWithQuery)
        {
            var data = _context.Players;
            if (getPlayersWithQuery.name != null)
            {
                IQueryable<Player> query = data.AsQueryable();
                query = query.Where(player => player.Name.ToLower() == getPlayersWithQuery.name.ToLower());
                return Ok(query.Select(p => new PlayerDto()
                {
                    Id= p.Id,
                    Name=p.Name,
                    Surname=p.Surname,
                    BackNumber=p.BackNumber,    
                    TeamId=p.TeamId,    
                }).ToList());
            }
            var result = data.Select(p => new PlayerDto()
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                BackNumber = p.BackNumber,
                TeamId = p.TeamId,
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Player([FromBody] PostPlayer postPlayer)
        {
            var dataTeams = _context.Teams;
            if (postPlayer.TeamId != null)
            {
                var isExistTeam = dataTeams.Any(t => t.Id == postPlayer.TeamId);
                if (!isExistTeam)
                {
                    return BadRequest("The team Id is not exist.");
                }
            }
            var data = _context.Players;
            Player player = new Player()
            {
                Name = postPlayer.Name,
                Surname = postPlayer.Surname,  
                BackNumber = postPlayer.BackNumber,
                TeamId = postPlayer?.TeamId,
                SavedDate = DateTime.UtcNow,
            };
            data.Add(player);
            _context.SaveChanges();
            _logger.LogInformation($"{player.Id} Id with {player.Name} is added.");
            return CreatedAtAction(nameof(PlayerFromRoot), new { id = player.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Player([FromRoute] int id)
        {
            var data = _context.Players;
            var result = data.Where(p => p.Id == id);
            var isExist = result.Any();
            if (!isExist)
            {
                return BadRequest("The Id is not exist.");
            }
            var player = result.SingleOrDefault();
            data.Remove(player);
            _context.SaveChanges();
            return Ok("Player has been deleted succesfully.");
        }

        [HttpPut("{id}")]
        public IActionResult Player(int id, [FromBody] UpdatePlayer updatedPlayer)
        {
            var dataTeams = _context.Teams;
            if (updatedPlayer.TeamId != null)
            {
                var isExistTeam = dataTeams.Any(t => t.Id == updatedPlayer.TeamId);
                if (!isExistTeam)
                {
                    return BadRequest("The team Id is not exist.");
                }
            }
            var dataPlayers = _context.Players;
            var players = dataPlayers.Where(p => p.Id == id);
            var isExistPlayer = players.Any();
            if (!isExistPlayer)
            {
                return BadRequest("The player Id is not exist.");
            }
            var player = players.SingleOrDefault();
            player.Name = updatedPlayer.Name != "string" ? updatedPlayer.Name : player.Name;
            player.Surname = updatedPlayer.Surname != "string" ? updatedPlayer.Surname : player.Surname;
            player.BackNumber = updatedPlayer.BackNumber != default ? updatedPlayer.BackNumber : player.BackNumber;
            player.TeamId = updatedPlayer?.TeamId != default ? updatedPlayer?.TeamId : player.TeamId;
            player.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
            return Ok("Player has been updated succesfully");
        }

        [HttpPatch("{id}")]
        public IActionResult Player(int id, [FromBody] PatchPlayerBackNumberRequest patchPlayerBackNumberRequest)
        {
            var data = _context.Players;
            var players = data.Where(p => p.Id == id);
            var isExist = players.Any();
            if (!isExist)
            {
                return BadRequest("The Id is not exist.");
            }
            var player = players.SingleOrDefault();
            player.BackNumber = patchPlayerBackNumberRequest.BackNumber;
            _context.SaveChanges();
            return Ok("Player back number has been updated succesfully.");
        }

    }
}
