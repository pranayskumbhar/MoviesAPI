using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MoviesAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext([NotNullAttribute]DbContextOptions<ApplicationDBContext> option):base(option)
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
