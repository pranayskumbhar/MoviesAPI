using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using Repository;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    public class GenresController
    {
        private readonly IRepository repository;



        public GenresController(IRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        [Route("api/genres1")]
        public List<Genre> Get()
        {
            return repository.GetAllGenres();
        }


        [HttpPost]
        [Route("api/genres2")]
        public List<Genre> Post()
        {
            return repository.GetAllGenres();
        }


        [HttpGet]
        [Route("api/genres3")]
        public Genre Get(int id)
        {
            return repository.GetAllGenres().Find(x => x.Id == id)!;

        }


        [HttpPut]
        [Route("api/genres4")]
        public List<Genre> Put()
        {
            return repository.GetAllGenres();
        }


        [HttpDelete]
        [Route("api/genre5")]
        public List<Genre> Delete()
        {
            return repository.GetAllGenres();
        }

    }
}
