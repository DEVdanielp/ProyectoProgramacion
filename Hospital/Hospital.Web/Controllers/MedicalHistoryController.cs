using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Hospital.Web.Controllers
{
    public class MedicalHistoryController : Controller
    {
        readonly IMedicalHistoryServices _medicalhistoryService;

        private readonly INotyfService _notifyService;
        public MedicalHistoryController(IMedicalHistoryServices medicalhistoryServices, INotyfService notifyService)
        {
            _medicalhistoryService = medicalhistoryServices;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            Response<List<MedicalHistory>> response = await _medicalhistoryService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

           
            MedicalHistoryDTO dto = await _medicalhistoryService.CreateDTO();
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalHistoryDTO medicalhistoryDTO)
        {
          
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debes completar los campos ");
                    return View(medicalhistoryDTO);
                }

                // Mapeo explícito del DTO a la entidad MedicalHistory
                MedicalHistory medicalHistory = new MedicalHistory
                {
                    NamePatient = medicalhistoryDTO.NamePatient,
                    Description = medicalhistoryDTO.Description,
                    AppoimentId = medicalhistoryDTO.AppoimentId
                };

                Response<MedicalHistory> response = await _medicalhistoryService.CreateAsync(medicalHistory);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado la Historia Clinica con Èxito");
                    return RedirectToAction(nameof(Index));
                }

                return View(medicalhistoryDTO);
            }
            catch (Exception ex)
            {
                return View(medicalhistoryDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {
         
            Response<MedicalHistory> response = await _medicalhistoryService.GetAsync(Id);

            if (response.IsSuccess)
            {
                // Mapea el resultado a MedicalHistoryDTO
                MedicalHistoryDTO model = new MedicalHistoryDTO
                {
                    Id = response.Result.Id,
                    NamePatient = response.Result.NamePatient,
                    Description = response.Result.Description,
                    AppoimentId = response.Result.AppoimentId,
                    Appoiments = new List<SelectListItem>() // Asumiendo que tienes una lista de citas
                };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalHistoryDTO medicalhistoryDTO)
        {
          
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debes completar los campos ");
                    return View(medicalhistoryDTO);
                }

                // Mapea de vuelta el DTO a la entidad MedicalHistory
                MedicalHistory medicalHistory = new MedicalHistory
                {
                    Id = medicalhistoryDTO.Id,
                    NamePatient = medicalhistoryDTO.NamePatient,
                    Description = medicalhistoryDTO.Description,
                    AppoimentId = medicalhistoryDTO.AppoimentId
                };

                Response<MedicalHistory> response = await _medicalhistoryService.EditAsync(medicalHistory);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha editado la Historia Clìnica con Èxito");
                    return RedirectToAction(nameof(Index));
                }

                return View(medicalhistoryDTO);
            }
            catch (Exception ex)
            {
                return View(medicalhistoryDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           
            try
            {
                Response<MedicalHistory> response = await _medicalhistoryService.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha eliminado la Historia Clìnica con Èxito");
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
