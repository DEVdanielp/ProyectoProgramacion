using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class MedicalOrdersController : Controller
    {
        readonly IMedicalOrdersServices _medicalOrdersService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelpers _comboshelper;
        public MedicalOrdersController(IMedicalOrdersServices medicalOrdersServices, INotyfService notifyService, ICombosHelpers comboshelper)
        {
            _medicalOrdersService = medicalOrdersServices;
            _notifyService = notifyService;
            _comboshelper = comboshelper;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };

            Response<PaginationResponse<MedicalOrder>> response = await _medicalOrdersService.GetListAsync(request);
            return View(response.Result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MedicalOrderDTO dto = new MedicalOrderDTO
            {
                Appoiments = await _comboshelper.GetComboAppoiments(),
                Medications = await _comboshelper.GetComboMedications(),
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MedicalOrderDTO medicalOrderdto)
        {
            try
            {

                Response<MedicalOrderDTO> response = await _medicalOrdersService.CreateAsync(medicalOrderdto);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado la Orden Medica con Èxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(medicalOrderdto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                Response<MedicalOrderDTO> response = await _medicalOrdersService.ToDtoAsync(id);
                if (!response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(response.Result);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MedicalOrderDTO medicalorderdto)
        {
            try
            {

                Response<MedicalOrderDTO> response = await _medicalOrdersService.EditAsync(medicalorderdto);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha actualizado con Èxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(medicalorderdto);
            }

        }
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                Response<MedicalOrder> response = await _medicalOrdersService.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha eliminado con Èxito");
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