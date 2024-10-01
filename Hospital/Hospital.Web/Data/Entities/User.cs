using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class User
    {
        [Key]
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

        public List<Appoiment>? AppoimentPatient { get; set; }
        public List<Appoiment>? AppoimentDoctor { get; set; }

        public  Rol Rol { get; set; }
     
        public int RolId { get; set; }
        
    }
}
