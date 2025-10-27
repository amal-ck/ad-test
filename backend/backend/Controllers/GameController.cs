using backend.Data;
using backend.dto;
using backend.Models;
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

        [HttpGet("getgames")]
        public IActionResult GetGames()
        {
            var games = _context.Games.ToList();
            return Ok(games);
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
