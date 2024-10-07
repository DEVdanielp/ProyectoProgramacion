using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class AppoimentsController : Controller
    {
        private readonly IAppoimentServices _appoimentsService;
        private readonly INotyfService _notifyService;

        public AppoimentsController(IAppoimentServices appoimentsService, INotyfService notifyService)
        {
            _appoimentsService = appoimentsService;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<Appoiment>> response = await _appoimentsService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AppoimentDTO dto = await _appoimentsService.CreateDTO();
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
                    _notifyService.Success("Se ha agendado con Èxito");
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
                    _notifyService.Success("Se ha actualizado con Èxito");
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
                _notifyService.Success("Se ha eliminado con Èxito");
                await _appoimentsService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
