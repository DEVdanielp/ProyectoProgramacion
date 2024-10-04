using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class RolPermissionsController : Controller
    {
        readonly IRolPermissionsServices _rpService;

        public RolPermissionsController(IRolPermissionsServices rp)
        {
            _rpService = rp;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<RolesPermission>> response = await _rpService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RolPermissionsDTO dto = await _rpService.CreateDTO();
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Create(RolPermissionsDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                Response<RolesPermission> response = await _rpService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

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
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

    }

 
}
