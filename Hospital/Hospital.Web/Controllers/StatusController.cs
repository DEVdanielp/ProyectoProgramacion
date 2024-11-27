using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Core.Attributes;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        private readonly IStatusServices _statusService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelpers _comboshelper;

        public StatusController(IStatusServices statusService, INotyfService notifyService, ICombosHelpers comboshelper)
        {
            _statusService = statusService;
            _notifyService = notifyService;
            _comboshelper = comboshelper;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showStatu", module: "Estado")]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery]int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 5,
                Page = Page ?? 1,
                Filter = Filter
            };

            Response<PaginationResponse<Status>> response = await _statusService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createStatu", module: "Estado")]
        public async Task<IActionResult> Create()
        {
            StatusDTO dto = new StatusDTO
            {
                Appoiment = await _comboshelper.GetComboAppoiments()
            };
            return View(dto);

        }

        [HttpPost]
        [CustomAuthorize(permission: "createStatu", module: "Estado")]
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
        [CustomAuthorize(permission: "updateStatu", module: "Estado")]
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
        [CustomAuthorize(permission: "updateStatu", module: "Estado")]
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
        [CustomAuthorize(permission: "deleteStatu", module: "Estado")]
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
