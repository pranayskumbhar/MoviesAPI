using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Boigraphy { get; set; }
        public string Picture { get; set; }
    }
}
