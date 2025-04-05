using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<Genre> _genre;
        public InMemoryRepository()
        {
            _genre = new List<Genre>()
                {
                    new Genre(){ Id = 1, Name ="Inception" },
                    new Genre(){ Id = 2, Name ="Harry Potter" },
                    new Genre(){ Id = 11, Name ="Pranay Kumbhar" },
                    new Genre(){ Id = 12, Name ="Lets Rock" },
                };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetAllGenres()
        {
            return _genre;
        }

    }
}
