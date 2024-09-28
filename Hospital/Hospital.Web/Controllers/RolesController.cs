using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Rol>> response = await _rolesService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]   
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rol rol)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rol);
                }

                Response<Rol> response = await _rolesService.CreateAsync(rol);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch(Exception ex)
            {
                return View(rol);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {
            Console.WriteLine("aaaa" + Id + "aaaa");
            Response<Rol> response = await _rolesService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Rol rol)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rol);
                }

                Response<Rol> response = await _rolesService.EditAsync(rol);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return View(rol);
            }
        }


    }
}
