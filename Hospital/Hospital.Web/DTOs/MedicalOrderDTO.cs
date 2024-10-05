using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class MedicalOrderDTO
    {
        public int Id { get; set; }
        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Description { get; set; }
        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Diagnosis { get; set; }

        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public IEnumerable<SelectListItem>? Medications { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Medicamento")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int MedicationId { get; set; }
        public IEnumerable<SelectListItem>? Appoiments { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Cita")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int AppoimentId { get; set; }
    }
}

