using System.ComponentModel.DataAnnotations;

namespace FullStackDeveloperTask.Api.Options
{
    public class CorsOptions
    {
        public const string Cors = "Cors";

        [Required]
        public required string Origin { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
