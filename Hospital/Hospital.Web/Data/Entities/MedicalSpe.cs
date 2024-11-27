using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class MedicalSpe
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; }

        public User UserDoctor { get; set; }
        public string? UserDoctorId { get; set; }

    }
}
