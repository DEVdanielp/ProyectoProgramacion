using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class RolPermissionsDTO
    {
        public IEnumerable<SelectListItem>? Rol { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Doctor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int? rolId { get; set; }

        public IEnumerable<SelectListItem>? Permisos { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Doctor")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int? PermisosId { get; set; }

    }
}
