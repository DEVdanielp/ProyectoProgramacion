using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class MedicalOrder
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Description { get; set; }
        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Diagnosis { get; set; }

        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public Medication? Medication { get; set; }
        public int MedicationId { get; set; }

        public Appoiment? Appoiment { get; set; }
        public int AppoimentId { get; set; }
    }
}
