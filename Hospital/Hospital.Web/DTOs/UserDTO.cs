using Hospital.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public int HospitalRoleId { get; set; }

        public HospitalRole HospitalRole { get; set; }
    }
}
