using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreCreationDTO
    {
        [StringLength(10)]
        [FirstLetterUppercase]
        public string? Name { get; set; }
    }
}
