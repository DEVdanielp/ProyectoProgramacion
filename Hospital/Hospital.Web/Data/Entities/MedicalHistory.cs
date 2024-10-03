using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [Display(Name = "Nombre De Paciente")]
        public string? NamePatient { get; set; }



        // Relación con Appoiment (Cita)
        public Appoiment? Appoiments { get; set; }
        public int? AppoimentId { get; set; }
    }
}
