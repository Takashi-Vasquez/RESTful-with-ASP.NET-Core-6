using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Autor>>> GetListAll()
        {
            return await context.Autores.Include(x => x.books).ToListAsync();
        }

        [HttpGet("first")]
        public async Task<ActionResult<Autor>> firstAutor([FromHeader] int miValor, [FromQuery] string nombre)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2=persona}/{opcional?}")]
        public async Task<ActionResult<Autor>> GetAutorById(int id, string param2, string opcional)
        {
            return await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Autor>> GetAutorByName([FromRoute] string name)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Name.Contains(name));
            if (autor == null) return NotFound();

            return Ok(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/autores/1
        public async Task<ActionResult> Upgrade(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var hasAutor = await context.Autores.AnyAsync(x => x.Id == id);
            if (!hasAutor)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var hasAutor = await context.Autores.AnyAsync(x => x.Id == id);
            if (!hasAutor)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
