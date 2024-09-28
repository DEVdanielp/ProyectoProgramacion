using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Roles
    {
        [Key]
        [Display(Name = "Rol")]
        public string Nombrerol  { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }
    }
}
