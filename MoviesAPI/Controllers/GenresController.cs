using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;
using Repository;
//using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
        public async Task<List<GenreDTO>> GetAsync([FromQuery] PaginationDTO paginationDTO)
        {
            try
            {
                var queryable = _context.Genres.AsQueryable();
                await HttpContext.InsertParametersPaginationInHeader(queryable);
                var ganres = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
                return mapper.Map<List<GenreDTO>>(ganres);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        //api/genres        
        // Method : POST
        public async Task<IActionResult> POST([FromBody] GenreCreationDTO genreCreationDTO)
        {

            Logger.LogIntoText();

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


        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
                if (genre == null)
                {
                    return NotFound();
                }
                return mapper.Map<GenreDTO>(genre);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {

            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
                if (genre == null)
                {
                    return NotFound();
                }
                genre = mapper.Map(genreCreationDTO, genre);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exists = await _context.Genres.AnyAsync(x => x.Id == id);
                if (!exists)
                {
                    return NotFound();
                }
                _context.Remove(new Genre() { Id = id });
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
