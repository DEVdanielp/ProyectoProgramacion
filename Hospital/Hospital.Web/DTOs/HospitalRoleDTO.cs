using Hospital.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class HospitalRoleDTO
    {


        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        public ICollection<RolePermission> RolePermissions { get; set; }

        public  List<PermissionForDTO> Permissions { get; set; }
        public string? PermissionIds { get; set; }


    }
}