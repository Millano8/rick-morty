using System.Diagnostics.CodeAnalysis;

namespace rick_morty_webapp.Models
{
    public class Characters
    {
        [NotNull]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public Uri Image { get; set; }
    }
}
