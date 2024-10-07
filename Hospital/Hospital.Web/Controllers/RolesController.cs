using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly IRolesServices _rolesService;
        private readonly INotyfService _notifyService;

        public RolesController(IRolesServices rolesService, INotyfService notifyService)
        {
            _rolesService = rolesService;
            _notifyService = notifyService;
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
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(rol);
                }

                Response<Rol> response = await _rolesService.CreateAsync(rol);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado este rol con éxito");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(rol);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {

            Response<Rol> response = await _rolesService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Rol rol)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(rol);
                }

                Response<Rol> response = await _rolesService.EditAsync(rol);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha actualizado este rol con éxito");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(rol);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                await _rolesService.DeleteAsync(id);
                _notifyService.Success("Se ha eliminado con éxito");
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }


    }
}
