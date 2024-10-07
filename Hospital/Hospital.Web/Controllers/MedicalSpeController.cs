using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;

namespace Hospital.Web.Controllers
{
    public class MedicalSpeController: Controller
    {     
        private readonly IMedicalSpeServices _medicalspeService;

        private readonly INotyfService _notifyService;

        public MedicalSpeController(IMedicalSpeServices medicalpsaService, INotyfService notifyService)
        {
            _medicalspeService = medicalpsaService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<MedicalSpe>> response = await _medicalspeService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MedicalSpeDTO dto = await _medicalspeService.CreateDTO();
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
                    return View(udto);
                }

                Response<MedicalSpe> response = await _medicalspeService.CreateAsync(udto);
                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado la especialidad médica con éxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
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
                    _notifyService.Success("Se ha actualizado con éxito");
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
                _notifyService.Success("Se ha eliminado con éxito");
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
