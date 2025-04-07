using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using Repository;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly ApplicationDBContext _context;
        private readonly IMapper mapper;



        public GenresController(IRepository repository, ApplicationDBContext context, IMapper _mapper)
        {
            this.repository = repository;
            _context = context;
            mapper = _mapper;
        }


        [HttpGet]
        //api/genres
        // Method : GET
        public async Task<List<GenreDTO>> GetAsync()
        {
            try
            {
                var ganres = await _context.Genres.ToListAsync();
                return mapper.Map<List<GenreDTO>>(ganres);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        //api/genres        
        // Method : POST
        public async Task<IActionResult> POST([FromBody] GenreCreationDTO genreCreationDTO)
        {
            try
            {
                var genre = mapper.Map<Genre>(genreCreationDTO);
                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
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
