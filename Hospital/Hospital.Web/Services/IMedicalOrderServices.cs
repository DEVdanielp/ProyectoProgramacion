﻿using Hospital.Web.Data.Entities;
using Hospital.Web.Data;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;
using Hospital.Web.Core.Pagination;

namespace Hospital.Web.Services
{
    public interface IMedicalOrdersServices
    {
        public Task<Response<PaginationResponse<MedicalOrder>>> GetListAsync(PaginationRequest request);
        public Task<Response<MedicalOrder>> GetAsync(int Id);
        public Task<Response<MedicalOrderDTO>> EditAsync(MedicalOrderDTO model);
        public Task<Response<MedicalOrderDTO>> CreateAsync(MedicalOrderDTO model);
        public Task<Response<MedicalOrder>> DeleteAsync(int id);
        public Task<Response<MedicalOrderDTO>> GetListDtoAsync();
        public Task<Response<MedicalOrderDTO>> ToDtoAsync(int id);

    }

    public class MedicalOrdersServices : IMedicalOrdersServices
    {
        private readonly DataContext _context;

        private readonly IConverterHelper _convertHelper;
        private readonly ICombosHelpers _combosHelpers;
        public MedicalOrdersServices(DataContext context, IConverterHelper convertHelper, ICombosHelpers combosHelpers)
        {
            _context = context;
            _convertHelper = convertHelper;
            _combosHelpers = combosHelpers;
        }


        public async Task<Response<MedicalOrderDTO>> CreateAsync(MedicalOrderDTO medicalOrderdto)
        {
            try
            {
                MedicalOrder medicalorder = _convertHelper.ToMedicalOrder(medicalOrderdto);
                await _context.MedicalOrders.AddAsync(medicalorder);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalOrderDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<MedicalOrder>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<MedicalOrder> query = _context.MedicalOrders.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    if (int.TryParse(request.Filter, out int filterId))
                    {
                        query = query.Where(s => s.Id == filterId);
                    }
                }

                PagedList<MedicalOrder> list = await PagedList<MedicalOrder>.ToPagedListAsync(query, request);

                PaginationResponse<MedicalOrder> result = new PaginationResponse<MedicalOrder>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<MedicalOrder>>.MakeResponseSuccess(result, "Especialidad Medica obtenidas con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<MedicalOrder>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalOrder>> GetAsync(int Id)
        {
            try
            {
                MedicalOrder? medicalOrder = await _context.MedicalOrders.FirstOrDefaultAsync(r => r.Id == Id);

                if (medicalOrder is null)
                {
                    return ResponseHelper<MedicalOrder>.MakeResponseFail("El id indicado no existe");
                }

                return ResponseHelper<MedicalOrder>.MakeResponseSuccess(medicalOrder);

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalOrder>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalOrderDTO>> EditAsync(MedicalOrderDTO medicalOrderdto)
        {
            try
            {
                MedicalOrder? medicalorder = await _context.MedicalOrders.FirstOrDefaultAsync(a => a.Id == medicalOrderdto.Id);

                medicalorder.Diagnosis = medicalOrderdto.Diagnosis;
                medicalorder.Description = medicalOrderdto.Description;
                medicalorder.Appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == medicalOrderdto.AppoimentId);
                medicalorder.Medication = await _context.Medications.FirstOrDefaultAsync(a => a.Id == medicalOrderdto.MedicationId);

                _context.MedicalOrders.Update(medicalorder);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto, "seccion actualizada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalOrderDTO>.MakeResponseFail(ex);
            }


        }
        public async Task<Response<MedicalOrder>> DeleteAsync(int id)
        {
            try
            {
                MedicalOrder? medicalorder = await _context.MedicalOrders.FirstOrDefaultAsync(a => a.Id == id);
                if (medicalorder is null)
                {
                    return ResponseHelper<MedicalOrder>.MakeResponseFail("El id indicado no existe");
                }
                _context.MedicalOrders.Remove(medicalorder);
                await _context.SaveChangesAsync();
                return ResponseHelper<MedicalOrder>.MakeResponseSuccess(medicalorder);


            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalOrder>.MakeResponseFail(ex);
            }
        }
        public async Task<Response<MedicalOrderDTO>> GetListDtoAsync()
        {
            MedicalOrderDTO? medicalOrderdto = new MedicalOrderDTO
            {

                Medications = await _combosHelpers.GetComboMedications(),

                Appoiments = await _combosHelpers.GetComboAppoiments()
            };
            return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto);

        }
        public async Task<Response<MedicalOrderDTO>> ToDtoAsync(int id)
        {
            Response<MedicalOrder> response = await GetAsync(id);
            MedicalOrderDTO? medicalOrderdto = new MedicalOrderDTO
            {
                Id = id,
                Description = response.Result.Description,
                Diagnosis = response.Result.Diagnosis,
                AppoimentId = response.Result.AppoimentId,
                MedicationId = response.Result.MedicationId,
                Medications = await _context.Medications.Select(a => new SelectListItem
                {
                    Text = $"{a.CommercialName} {a.Description}",
                    Value = a.Id.ToString()
                }).ToListAsync(),

                Appoiments = await _context.Appoiments.Select(a => new SelectListItem
                {
                    Text = $"Fecha:{a.Date}, Hora:{a.Time}, Número Cita: {a.Id}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
            };
            return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto);
        }
    }
}


