using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class MedicalHistoryDTO
    {
        public int Id { get; set; } // ID del historial médico

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [Display(Name = "Nombre del Paciente")]
        public string NamePatient { get; set; } // Nombre del paciente relacionado con el historial médico

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [Display(Name = "Descripción del historial")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } // Descripción del historial médico




        [Required(ErrorMessage = "Debe seleccionar una cita")]
        [Display(Name = "Cita asociada")]
        public int? AppoimentId { get; set; } // ID de la cita asociada

        // Lista de citas disponibles para seleccionar
        public List<SelectListItem>? Appoiments { get; set; }

    }
}
