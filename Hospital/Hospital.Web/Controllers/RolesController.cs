using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class RolesController : Controller
    {
        //Inyectamos la dependecia de la interfaz creada
        readonly IRolesServices _rolesService;

        public RolesController(IRolesServices rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<Rol>> response = await _rolesService.GetListAsync();
            return View(response.Result);
        }
    }
}
