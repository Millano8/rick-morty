using rick_morty_webapp.Models;


namespace rick_morty_webapp.Services.Interfaces
{
    public interface IRickMortyService
    {
        public Task<Characters> GetCharacter(string charachterId);
    }
}
