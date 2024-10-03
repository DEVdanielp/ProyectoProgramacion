using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre del permiso")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(32, ErrorMessage = "El nombre no puede exceder los 32 caracteres")]
        public string? Name { get; set; }
        public string? Description { get; set; }




    }


}

