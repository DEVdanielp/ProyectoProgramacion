using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String CommercialName{ get; set; }

        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String ScientificName{ get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Group { get; set; }
        //Analgésicos
        //Antiácidos y antiulcerosos
        //Antialérgicos
        //Antidiarreicos
        //Laxantes
        //Antiinflamatorios
        //Antiinfecciosos
        //Antipiréticos
        //Mucolíticos
        //Antitusivos
        [MaxLength(256, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String? Description { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener maximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public String Laboratory { get; set; }
        


    }
}
