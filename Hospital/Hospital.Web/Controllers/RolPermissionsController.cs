using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class RolPermissionsController : Controller
    {
        private readonly IRolPermissionsServices _rpService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelpers _combosHelpers;

        public RolPermissionsController(IRolPermissionsServices rp, INotyfService notifyService, ICombosHelpers combosHelpers)
        {
            _rpService = rp;
            _notifyService = notifyService;
            _combosHelpers = combosHelpers;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<RolesPermission>> response = await _rpService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RolPermissionsDTO dto = new RolPermissionsDTO
            {
                Permisos = await _combosHelpers.GetComboPermissions(),
                Rol = await _combosHelpers.GetComboRols(),    
            };
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Create(RolPermissionsDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(dto);
                }

                Response<RolesPermission> response = await _rpService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha asignado este permiso con éxito");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int rolId, int PermisosId)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                await _rpService.DeleteAsync(PermisosId, rolId);
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
