using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusServices _statusService;
        private readonly INotyfService _notifyService;

        public StatusController(IStatusServices statusService, INotyfService notifyService)
        {
            _statusService = statusService;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<Status>> response = await _statusService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StatusDTO dto = await _statusService.CreateDTO();
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(dto);
                }

                Response<Status> response = await _statusService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado este estado con éxito");
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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {

            Response<StatusDTO> response = await _statusService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(StatusDTO status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(status);
                }

                Response<StatusDTO> response = await _statusService.EditAsync(status);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha actualizado con éxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(status);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                await _statusService.DeleteAsync(id);
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
