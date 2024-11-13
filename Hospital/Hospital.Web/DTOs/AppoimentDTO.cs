using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class AppoimentDTO
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public TimeOnly Time { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public DateOnly Date { get; set; }


        public IEnumerable<SelectListItem>? UserDoctor { get; set; }
        [Range(265, int.MaxValue, ErrorMessage = "Debe seleccionar un Doctor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string? UserDoctorId { get; set; }

        public IEnumerable<SelectListItem>? UserPatient { get; set; }
        [Range(265, int.MaxValue, ErrorMessage = "Debe seleccionar un Paciente")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string UserPatientId { get; set; }
    }
}
