using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Appoiment
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public TimeOnly Time { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public DateOnly Date { get; set; }


        public User UserPatient { get; set; }
        public Guid UserPatientId { get; set; }


        public User UserDoctor { get; set; }
        public Guid UserDoctorId { get; set; }
    }
}
