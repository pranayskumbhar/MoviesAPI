using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using Repository;
using System.Drawing.Printing;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IFileOperation fileOperation;
        public ActorsController(ApplicationDBContext context, IMapper mapper, IFileOperation fileOperation)
        {
            this.context = context;
            this.fileOperation = fileOperation;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            return mapper.Map<List<ActorDTO>>(await context.Actors.ToListAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            try
            {
                var actor = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);
                if (actor == null)
                {
                    return NotFound();
                }
                return mapper.Map<ActorDTO>(actor);
            }
            catch (Exception ex)
            {
                Logger.LogIntoText();
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ActorDTO>> Post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            try
            {
                var actor = mapper.Map<Actor>(actorCreationDTO);
                actor.Picture = await fileOperation.GetBase64(actorCreationDTO.Picture) ?? "";
                await context.Actors.AddAsync(actor);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<ActorDTO>> Put([FromForm] ActorCreationDTO actorCreationDTO)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var actor = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);
                if (actor == null)
                {
                    return NotFound();
                }

                context.Remove(actor);
                await context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                Logger.LogIntoText();
                throw;
            }
        }
    }
}
