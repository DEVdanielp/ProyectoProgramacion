﻿using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.Data.Entities
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NameRol")]
        public string NameRol { get; set; }
        public List<RolesPermission>? RolPermisos { get; set; }
    }
}
