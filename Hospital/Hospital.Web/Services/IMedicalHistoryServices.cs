using Hospital.Web.Core;
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
        Task<Response<List<MedicalHistory>>> GetListAsync();
        Task<Response<MedicalHistoryDTO>> GetOneAsync(int Id);
        Task<Response<MedicalHistory>> EditAsync(MedicalHistory model);
        Task<Response<MedicalHistory>> CreateAsync(MedicalHistoryDTO model);
        public Task<MedicalHistoryDTO> CreateDTO();
        Task<Response<MedicalHistory>> DeleteAsync(int id);
    }

    public class MedicalHistoryService : IMedicalHistoryServices
    {
        private readonly DataContext _context;

        private readonly IConvertHelper _convertHelper;
        public MedicalHistoryService(DataContext context, IConvertHelper convertHelper)
        {
            _context = context;
            _convertHelper = convertHelper;
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

        public async Task<MedicalHistoryDTO> CreateDTO()
        {
            MedicalHistoryDTO dto = new MedicalHistoryDTO
            {
                Appoiment = await _context.Appoiments.Select(a => new SelectListItem
                {
                    Text = $"Fecha:{a.Date}, Hora:{a.Time}, Doctor: {a.UserDoctorId}",
                    Value = a.Id.ToString()
                }).ToListAsync()

            };
            return dto;
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

                    Appoiment = await _context.Appoiments.Select(a => new SelectListItem
                    {
                        Text = $"Fecha: {a.Date}, Hora: {a.Time}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),

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
