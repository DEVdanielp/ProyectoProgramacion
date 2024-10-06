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
    public class PermissionsController : Controller
    {


        readonly IPermissionsServices _permissionsService;

        private readonly INotyfService _notifyService;

        public PermissionsController(IPermissionsServices permissionsService, INotyfService notifyService)
        {
            _permissionsService = permissionsService;
            _notifyService = notifyService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Permissions>> response = await _permissionsService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Permissions permissions)
        {
            _notifyService.Success("Se ha creado el Permiso con Èxito");
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(permissions);
                }

                Response<Permissions> response = await _permissionsService.CreateAsync(permissions);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return View(permissions);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {
        
            Response<Permissions> response = await _permissionsService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Permissions permissions)
        {
            _notifyService.Success("Se ha editado el Permiso con Èxito");
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(permissions);
                }

                Response<Permissions> response = await _permissionsService.EditAsync(permissions);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return View(permissions);
            }
        
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _notifyService.Success("Se ha eliminado con Èxito");
            try
            {
                Response<Permissions> response = await _permissionsService.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));

            }
            
        }


    }
}