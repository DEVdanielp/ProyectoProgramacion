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
        public MedicationsController(IMedicationsServices medicationsServices)
        {
            _medicationsService = medicationsServices;
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
                    return View(medication);
                }

                Response<Medication> response = await _medicationsService.CreateAsync(medication);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

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

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Medication medication)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(medication);
                }

                Response<Medication> response = await _medicationsService.EditAsync(medication);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

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
