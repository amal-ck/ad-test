using backend.Data;
using backend.dto;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PurchaseController : ControllerBase
    {
        private AppDbContext _context;
        public PurchaseController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [Authorize]
        [HttpPost("purchaseGame")]
        public IActionResult PurchaseGame([FromBody] int GameId)
        {
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            var user = _context.Users.Find(userId);
            var game = _context.Games.Find(GameId);
            MdlResponse mdlResponse = new MdlResponse();

            if (user == null || game == null) {
                //MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Purchase Failed";
                mdlResponse.ErrorMsg = "User or Game is Invalid";
                return BadRequest(mdlResponse);
            }

            bool alreadyOwned = _context.UserGameLibraries.Any(ug => ug.userId == userId && ug.GameId == GameId);
            if (alreadyOwned) {
                //MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Purchase Failed";
                mdlResponse.ErrorMsg = "Already in Library";
                return BadRequest(mdlResponse);
            }

            var userLibrary = new UserGameLibrary
            {
                userId = userId,
                GameId = GameId,
                priceAtPurchase = game.price,
                PurchasedOn = DateTime.UtcNow
            };

            _context.UserGameLibraries.Add(userLibrary);
            _context.SaveChanges();


            mdlResponse.Success = true;
            mdlResponse.Message = "Purchase Success";
            return Ok(mdlResponse);
        }

        [Authorize]
        [HttpGet("getUserLibrary")]
        public IActionResult GetUserLibrary()
        {
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
            var library = _context.UserGameLibraries.Where(ug => ug.userId == userId).Include(ug => ug.Game)
                .Select(ug => new
                {
                    ug.Game.name,
                    ug.priceAtPurchase,
                    ug.TotalPlaytime,
                    ug.PurchasedOn,
                    ug.GameId
                });

            return Ok(library);
        }
    }
}
