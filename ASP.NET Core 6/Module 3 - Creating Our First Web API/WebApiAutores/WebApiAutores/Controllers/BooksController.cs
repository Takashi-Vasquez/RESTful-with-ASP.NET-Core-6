using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await context.Books.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> createBook(Book book)
        {
            var hasAutor = await context.Autores.AnyAsync(x => x.Id == book.AutorId);

            if (!hasAutor)
            {
                return BadRequest($"No existe el autor de Id: {book.AutorId} ");
            }

            context.Add(book);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
