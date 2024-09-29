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
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un autor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int UserDoctorId { get; set; }

        public IEnumerable<SelectListItem>? UserPatient { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un autor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int UserPatientId { get; set; }
    }
}
