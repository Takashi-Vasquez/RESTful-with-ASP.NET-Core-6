namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Libro> books { get; set; }
    }
}
