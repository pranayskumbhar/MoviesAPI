using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;
namespace MoviesAPI.Entities
{
    //public class Genre : IValidatableObject
    public class Genre 
    { 
        public int Id { get; set; }

        [FirstLetterUppercase]
        public string? Name { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    yield return new ValidationResult("First letter should be uppercase", new string[] { nameof(Name) });
        //}
    }
}
