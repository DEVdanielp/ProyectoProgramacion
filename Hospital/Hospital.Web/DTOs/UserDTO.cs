using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string FirstName { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string LastName { get; set; }

        public DateOnly Birth { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string UserName { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Password { get; set; }

        public IEnumerable<SelectListItem>? Rol { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un autor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int RolId { get; set; }
    }
}
