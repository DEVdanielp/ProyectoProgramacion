using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class MedicationsController : Controller
    {
        readonly IMedicationsServices _medicationsService;
        private readonly INotyfService _notifyService;
        public MedicationsController(IMedicationsServices medicationsServices, INotyfService notifyService)
        {
            _medicationsService = medicationsServices;
            _notifyService = notifyService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Medication>> response = await _medicationsService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Medication medication)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(medication);
                }

                Response<Medication> response = await _medicationsService.CreateAsync(medication);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado el Medicamento con Èxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(medication);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {
            Response<Medication> response = await _medicationsService.GetAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Medication medication)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(medication);
                }

                Response<Medication> response = await _medicationsService.EditAsync(medication);

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
                return View(medication);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                Response<Medication> response = await _medicationsService.DeleteAsync(id);
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
