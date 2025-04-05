using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public interface IRepository : IDisposable
    {
        List<Genre> GetAllGenres();
    }
}
