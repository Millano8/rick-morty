using Microsoft.AspNetCore.Mvc;
using rick_morty_webapp.Models;
using rick_morty_webapp.Services.Interfaces;

namespace rick_morty_webapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RickMortyController : ControllerBase
    {
        private readonly IRickMortyService _rickMortyService;       
        public RickMortyController(IRickMortyService rickMortyService)
        {
            _rickMortyService = rickMortyService;
        }


        [HttpGet("{characterId}")]
        public async Task<ActionResult<Characters>> GetCharacter(string characterId)
        {
            try
            {
                var character = await _rickMortyService.GetCharacter(characterId);
                

                if (character == null)
                {
                    return NotFound($"Character with ID {characterId} not found");
                }

                return Ok(character);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while fetching the character");
            }
        }
    }
}
