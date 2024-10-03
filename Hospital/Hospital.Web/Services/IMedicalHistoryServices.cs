using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Web.Services
{
    public interface IMedicalHistoryServices
    {
        Task<Response<List<MedicalHistory>>> GetListAsync();
        Task<Response<MedicalHistory>> GetAsync(int Id);
        Task<Response<MedicalHistory>> EditAsync(MedicalHistory model);
        Task<Response<MedicalHistory>> CreateAsync(MedicalHistory model);
        Task<Response<MedicalHistory>> DeleteAsync(int id);
    }

    public class MedicalHistoryService : IMedicalHistoryServices
    {
        private readonly DataContext _context;

        public MedicalHistoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<MedicalHistory>> CreateAsync(MedicalHistory model)
        {
            try
            {
                MedicalHistory medicalhistory = new MedicalHistory
                {
                    NamePatient = model.NamePatient,
                    Description = model.Description,
                    AppoimentId = model.AppoimentId
                };

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

        public async Task<Response<MedicalHistory>> GetAsync(int Id)
        {
            try
            {
                MedicalHistory medicalhistory = await _context.MedicalHistory.FirstOrDefaultAsync(r => r.Id == Id);

                if (medicalhistory == null)
                {
                    return ResponseHelper<MedicalHistory>.MakeResponseFail("El id indicado no existe");
                }

                return ResponseHelper<MedicalHistory>.MakeResponseSuccess(medicalhistory);
            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalHistory>.MakeResponseFail(ex);
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
    }
}
