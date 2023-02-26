using System.ComponentModel.DataAnnotations;

namespace UrlShortener.WebApplication.Models
{
    public class UrlModel
    {
        [Required]
        [MinLength(10)]
        [Url]
        public string Url { get; set; } = string.Empty;
    }
}
