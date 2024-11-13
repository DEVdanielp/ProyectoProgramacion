using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Web.Services
{
    public interface IMedicalHistoryServices
    {
        public  Task<Response<List<MedicalHistory>>> GetListAsync();
        public Task<Response<MedicalHistoryDTO>> GetOneAsync(int Id);
        public Task<Response<MedicalHistory>> EditAsync(MedicalHistory model);
        public Task<Response<MedicalHistory>> CreateAsync(MedicalHistoryDTO model);
        public Task<Response<MedicalHistory>> DeleteAsync(int id);
        public Task<Response<PaginationResponse<MedicalHistory>>> GetListAsync(PaginationRequest request);
    }

    public class MedicalHistoryService : IMedicalHistoryServices
    {
        private readonly DataContext _context;

        private readonly IConverterHelper _convertHelper;
        private readonly ICombosHelpers _combos;
        public MedicalHistoryService(DataContext context, IConverterHelper convertHelper, ICombosHelpers combos)
        {
            _context = context;
            _convertHelper = convertHelper;
            _combos = combos;
        }

        public async Task<Response<MedicalHistory>> CreateAsync(MedicalHistoryDTO model)
        {
            try
            {
                MedicalHistory medicalhistory = _convertHelper.ToMedicalHistory(model);  
                await _context.MedicalHistory.AddAsync(medicalhistory);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalHistory>.MakeResponseSuccess(medicalhistory, "Sección creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalHistory>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<MedicalHistory>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<MedicalHistory> query = _context.MedicalHistory.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.NamePatient.ToLower().Contains(request.Filter.ToLower())
                                          || s.Description.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<MedicalHistory> list = await PagedList<MedicalHistory>.ToPagedListAsync(query, request);

                PaginationResponse<MedicalHistory> result = new PaginationResponse<MedicalHistory>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<MedicalHistory>>.MakeResponseSuccess(result, "Historias Médicas obtenidas con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<MedicalHistory>>.MakeResponseFail(ex);
            }
        }
        public async Task<Response<List<MedicalHistory>>> GetListAsync()
        {
            try
            {
                List<MedicalHistory> medicalHistories = await _context.MedicalHistory.ToListAsync();
                return ResponseHelper<List<MedicalHistory>>.MakeResponseSuccess(medicalHistories);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<MedicalHistory>>.MakeResponseFail(ex);
            }
        }



        public async Task<Response<MedicalHistory>> EditAsync(MedicalHistory model)
        {
            try
            {
                _context.MedicalHistory.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalHistory>.MakeResponseSuccess(model, "Sección actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalHistory>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalHistory>> DeleteAsync(int id)
        {
            try
            {
                MedicalHistory medicalhistory = await _context.MedicalHistory.FirstOrDefaultAsync(a => a.Id == id);
                if (medicalhistory == null)
                {
                    return ResponseHelper<MedicalHistory>.MakeResponseFail("El id indicado no existe");
                }

                _context.MedicalHistory.Remove(medicalhistory);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalHistory>.MakeResponseSuccess(medicalhistory);
            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalHistory>.MakeResponseFail(ex);
            }
        }
        public async Task<Response<MedicalHistoryDTO>> GetOneAsync(int Id)
        {
            try
            {
                MedicalHistory? status = await _context.MedicalHistory.FirstOrDefaultAsync(a => a.Id == Id);

                if (status is null)
                {
                    return ResponseHelper<MedicalHistoryDTO>.MakeResponseFail("El id indicado no existe");
                }

                MedicalHistoryDTO dto = new MedicalHistoryDTO
                {
                    Description = status.Description,
                    NamePatient = status.NamePatient,

                    Appoiment = await _combos.GetComboAppoiments()

                };
                return ResponseHelper<MedicalHistoryDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalHistoryDTO>.MakeResponseFail(ex);
            }
        }
    }
}
