using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutores.Entities
{

    public class Autor : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        //[CapitalLetter Attribute]
        public string Name { get; set; }
        [Range(18, 120)]
        [NotMapped]
        public int Age { get; set; }
        [CreditCard]
        public string TarjetaDeCredito { get; set; }
        [Url]
        public string URL { get; set; }
        public string Description { get; set; }

        public int Menor { get; set; }
        public int Mayor { get; set; }
        public List<Libro> books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firtletter = Name[0].ToString();
                if (firtletter != firtletter.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new string[] { nameof(Name) });
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Estevalor no puede ser más grande que el campo Mayor",
                   new string[] { nameof(Menor) });
            }
        }
    }
}
