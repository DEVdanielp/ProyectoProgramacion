﻿using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Helpers;
using Hospital.Web.Core.Attributes;
using Hospital.Web.Core.Pagination;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Web.Controllers
{
    [Authorize]
    public class MedicalHistoryController : Controller
    {
        readonly IMedicalHistoryServices _medicalhistoryService;
        private readonly ICombosHelpers _comboshelper;
        private readonly INotyfService _notifyService;
        public MedicalHistoryController(IMedicalHistoryServices medicalhistoryServices, INotyfService notifyService,ICombosHelpers comboshelper)
        {
            _medicalhistoryService = medicalhistoryServices;
            _notifyService = notifyService;
            _comboshelper = comboshelper;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showMedicalHistory", module: "Historia Clínica")]
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

            Response<PaginationResponse<MedicalHistory>> response = await _medicalhistoryService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createMedicalHistory", module: "Historia Clínica")]
        public async Task<IActionResult> Create()
        {


            MedicalHistoryDTO dto = new MedicalHistoryDTO
            {
                Appoiment = await _comboshelper.GetComboAppoiments()
            };
            return View(dto);   

        }

        [HttpPost]
        [CustomAuthorize(permission: "createMedicalHistory", module: "Historia Clínica")]
        public async Task<IActionResult> Create(MedicalHistoryDTO medicalhistoryDTO)
        {
          
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debes completar los campos ");
                    medicalhistoryDTO.Appoiment = await _comboshelper.GetComboAppoiments();
                    return View(medicalhistoryDTO);
                }

                // Mapeo explícito del DTO a la entidad MedicalHistory
                //MedicalHistory medicalHistory = new MedicalHistory
                //{
                //    NamePatient = medicalhistoryDTO.NamePatient,
                //    Description = medicalhistoryDTO.Description,
                //    AppoimentId = medicalhistoryDTO.AppoimentId
                //};

                Response<MedicalHistory> response = await _medicalhistoryService.CreateAsync(medicalhistoryDTO);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado la Historia Clinica con éxito");
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
        [CustomAuthorize(permission: "updateMedicalHistory", module: "Historia Clínica")]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {

            Response<MedicalHistoryDTO> response = await _medicalhistoryService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [CustomAuthorize(permission: "updateMedicalHistory", module: "Historia Clínica")]
        public async Task<IActionResult> Edit(MedicalHistoryDTO medicalhistoryDTO)
        {
          
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debes completar los campos ");
                    medicalhistoryDTO.Appoiment = await _comboshelper.GetComboAppoiments();
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
                    _notifyService.Success("Se ha editado la Historia Clìnica con éxito");
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
        [CustomAuthorize(permission: "deleteMedicalHistory", module: "Historia Clínica ")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           
            try
            {
                Response<MedicalHistory> response = await _medicalhistoryService.DeleteAsync(id);
                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha eliminado la Historia Clìnica con éxito");
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
