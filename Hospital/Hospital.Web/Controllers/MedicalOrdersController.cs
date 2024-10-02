using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class MedicalOrdersController : Controller
    {
        readonly IMedicalOrdersServices _medicalOrdersService;
        private readonly ILogger<MedicalOrdersController> _logger;
        public MedicalOrdersController(IMedicalOrdersServices medicalOrdersServices, ILogger<MedicalOrdersController> logger)
        {
            _medicalOrdersService = medicalOrdersServices;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<MedicalOrder>> response = await _medicalOrdersService.GetListAsync();
            return View(response.Result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Response<MedicalOrderDTO> response = await _medicalOrdersService.GetListDtoAsync();
            return View(response.Result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MedicalOrderDTO medicalOrderdto)
        {
            try
            {
                //_logger.LogInformation($"Diagnosis: {medicalOrderdto.Diagnosis}, Description: {medicalOrderdto.Description}, AppoimentId: {medicalOrderdto.IdAppoiment}, MedicationId: {medicalOrderdto.IdMedication}, Appoiments: {medicalOrderdto.Appoiments}, Medications: { medicalOrderdto.Medications}");

                //if (!ModelState.IsValid)
                //{
                //    _logger.LogInformation("------------------no esta siento valido----------------------");
                //    Response<MedicalOrderDTO> responseList = await _medicalOrdersService.GetListDtoAsync();
                //    return View(responseList.Result);
                //}

                Response<MedicalOrderDTO> response = await _medicalOrdersService.CreateAsync(medicalOrderdto);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

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
                //if (!ModelState.IsValid)
                //{
                //    return View();
                //}

                Response<MedicalOrderDTO> response = await _medicalOrdersService.EditAsync(medicalorderdto);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

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