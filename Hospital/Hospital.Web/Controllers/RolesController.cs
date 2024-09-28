using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            List<Rol> roles = new List<Rol>
            {
               new Rol
               {
                   NameRol = "medico",
                   Description = "aaa"
               },

               new Rol
               {
                   NameRol = "medicdsado",
                   Description = "aaafsf"
               }
            };
            return View(roles);
        }
    }
}
