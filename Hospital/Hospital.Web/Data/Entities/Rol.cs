using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Rol
    {
        [Key]
        [Display(Name = "Rol")]
        public string NameRol { get; set; } 

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Description { get; set; }
    }
}
