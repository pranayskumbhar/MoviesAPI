namespace MoviesAPI.Services
{
    public interface IFileOperation : IDisposable
    {
        Task<string> GetBase64(IFormFile file);
    }
}
