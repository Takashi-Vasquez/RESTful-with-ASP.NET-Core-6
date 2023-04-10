using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string Name { get; set; }
        [Range(18, 100)]
        [NotMapped]
        public int Age { get; set; }
        [CreditCard]
        public string TarjetaDeCredito { get; set; }
        [Url]
        public string URL { get; set; }
        public string Description { get; set; }
        public List<Libro> books { get; set; }
    }
}
