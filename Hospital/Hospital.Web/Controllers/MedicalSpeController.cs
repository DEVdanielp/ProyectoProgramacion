using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Helpers;
using Hospital.Web.Core.Pagination;
using Microsoft.AspNetCore.Authorization;


namespace Hospital.Web.Controllers
{
    [Authorize]
    public class MedicalSpeController : Controller
    {
        private readonly IMedicalSpeServices _medicalspeService;

        private readonly INotyfService _notifyService;

        private readonly ICombosHelpers _comboshelper;

        public MedicalSpeController(IMedicalSpeServices medicalpsaService, INotyfService notifyService, ICombosHelpers comboshelper)
        {
            _medicalspeService = medicalpsaService;
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

            Response<PaginationResponse<MedicalSpe>> response = await _medicalspeService.GetListAsync(request);
            return View(response.Result);
        }
    

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MedicalSpeDTO dto = new MedicalSpeDTO
            {
                UserDoctor = await _comboshelper.GetComboDoctor()
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalSpeDTO udto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    udto.UserDoctor = await _comboshelper.GetComboDoctor();
                    return View(udto);
                }

                Response<MedicalSpe> response = await _medicalspeService.CreateAsync(udto);
                if (!response.IsSuccess)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    udto.UserDoctor = await _comboshelper.GetComboDoctor();
                    return View(udto);
                }
                _notifyService.Success("Se ha creado el Usuario con Èxito");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(udto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<MedicalSpeDTO> response = await _medicalspeService.GetOneAsycn(id);
            if (response.IsSuccess)
            {

                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalSpeDTO section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(section);
                }
                Response<MedicalSpeDTO> response = await _medicalspeService.EditAsync(section);

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
                return View(section);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                _notifyService.Success("Se ha eliminado con Èxito");
                await _medicalspeService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
