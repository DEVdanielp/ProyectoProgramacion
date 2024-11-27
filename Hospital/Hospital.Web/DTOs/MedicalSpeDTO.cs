using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class MedicalSpeDTO
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; }
        public IEnumerable<SelectListItem>? UserDoctor { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Doctor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string UserDoctorId { get; set; }
    }
}
