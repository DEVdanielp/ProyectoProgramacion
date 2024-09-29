using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public string StatusAppoiment { get; set; }

        public Appoiment Appoiment { get; set; }
        public int? AppoimentId { get; set; }

    }
}
