using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs
{
    public class PermissionForDTO : Permission
    {
        public bool Selected { get; set; }
    }
}
