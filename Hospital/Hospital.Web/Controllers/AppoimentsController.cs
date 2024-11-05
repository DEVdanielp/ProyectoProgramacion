using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class AppoimentsController : Controller
    {
        private readonly IAppoimentServices _appoimentsService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelpers _comboshelper;

        public AppoimentsController(IAppoimentServices appoimentsService, INotyfService notifyService, ICombosHelpers comboshelper)
        {
            _appoimentsService = appoimentsService;
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
                RecordsPerPage = RecordsPerPage ?? 5,
                Page = Page ?? 1,
                Filter = Filter 
            };

            Response<PaginationResponse<Appoiment>> response = await _appoimentsService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AppoimentDTO dto = new AppoimentDTO
            {
                UserDoctor = await _comboshelper.GetComboDoctor(),
                UserPatient = await _comboshelper.GetComboPatient()
                
            };
            return View(dto);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppoimentDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(dto);
                }

                Response<Appoiment> response = await _appoimentsService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha asignado esta cita con éxito");
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

            Response<AppoimentDTO> response = await _appoimentsService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppoimentDTO appoiment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(appoiment);
                }

                Response<AppoimentDTO> response = await _appoimentsService.EditAsync(appoiment);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha actualizado esta cita con éxito");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(appoiment);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                await _appoimentsService.DeleteAsync(id);
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
