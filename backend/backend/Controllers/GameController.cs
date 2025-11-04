using backend.Data;
using backend.dto;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GameController: ControllerBase
    {
        private AppDbContext _context;
        public GameController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("getgames")]
        public IActionResult GetGames([FromQuery] int pageNumber, int pageSize)
        {
            var userId = int.Parse(User.FindFirst("userId").Value);
                
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 9;

            var totalRecords = _context.Games.Count();
            var userLibraryGameIds = _context.UserGameLibraries.Where(l => l.userId == userId).Select(l => l.GameId).ToHashSet();


            var games = _context.Games.Skip((pageNumber - 1) * pageSize).Take(pageSize).
                Select(g => new
                {
                    g.id,
                    g.name,
                    g.category,
                    g.price,
                    g.description,
                    Sold = userLibraryGameIds.Contains(g.id)
                }).ToList();

            var res = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = games
            };

            return Ok(res);
        }

        [HttpPost("savegames")]
        public IActionResult SaveGames([FromBody] DtoGame dtoGame)
        {
            var existingGame = _context.Games.Find(dtoGame.id);
            if (existingGame != null)
            {
                existingGame.name = dtoGame.name;
                existingGame.description = dtoGame.description;
                existingGame.category = dtoGame.category;
                _context.SaveChanges();
            }
            else
            {

                Game game = new Game
                {
                    name = dtoGame.name,
                    description = dtoGame.description,
                    category = dtoGame.category
                };
                _context.Games.Add(game);
                _context.SaveChanges();
                dtoGame.id = game.id;
            }
            return Ok(dtoGame);
        }

        [HttpGet("getgamesbyid/{id}")]
        public IActionResult SaveGames([FromRoute] int id)
        {
            var game = _context.Games.Find(id);
            _context.SaveChanges();
            return Ok(game);
        }

        [HttpDelete("deletegamesbyid/{id}")]
        public IActionResult DeleteGame([FromRoute] int id)
        {
            var game = _context.Games.Find(id);
            if(game != null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
            return Ok(game);
        }
    }
}
