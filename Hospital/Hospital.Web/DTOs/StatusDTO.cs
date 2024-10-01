using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class StatusDTO
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public string StatusAppoiment { get; set; } = null!;

        public IEnumerable<SelectListItem>? Appoiment { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Paciente")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int AppoimentId { get; set; }
    }
}
