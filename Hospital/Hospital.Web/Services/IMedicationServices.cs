using Hospital.Web.Data.Entities;
using Hospital.Web.Data;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core;

namespace Hospital.Web.Services
{
    public interface IMedicationsServices
    {
        public Task<Response<List<Medication>>> GetListAsync();
        public Task<Response<Medication>> GetAsync(int Id);
        public Task<Response<Medication>> EditAsync(Medication model);
        public Task<Response<Medication>> CreateAsync(Medication model);
        public Task<Response<Medication>> DeleteAsync(int id);

    }

    public class MedicationService : IMedicationsServices
    {
        private readonly DataContext _context;

        public MedicationService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Medication>> CreateAsync(Medication model)
        {
            try
            {
                Medication medication = new Medication
                {
                    CommercialName = model.CommercialName,
                    Description = model.Description,
                    ScientificName = model.ScientificName,
                    Laboratory = model.Laboratory,
                    Group = model.Group
                };

                await _context.Medications.AddAsync(medication);
                await _context.SaveChangesAsync();

                return ResponseHelper<Medication>.MakeResponseSuccess(medication, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Medication>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Medication>>> GetListAsync()
        {
            try
            {
                List<Medication> medications = await _context.Medications.ToListAsync();
                return ResponseHelper<List<Medication>>.MakeResponseSuccess(medications);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Medication>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Medication>> GetAsync(int Id)
        {
            try
            {
                Medication? medication = await _context.Medications.FirstOrDefaultAsync(r => r.Id == Id);

                if (medication is null)
                {
                    return ResponseHelper<Medication>.MakeResponseFail("El id indicado no existe");
                }

                return ResponseHelper<Medication>.MakeResponseSuccess(medication);

            }
            catch (Exception ex)
            {
                return ResponseHelper<Medication>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Medication>> EditAsync(Medication model)
        {
            try
            {
                _context.Medications.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Medication>.MakeResponseSuccess(model, "seccion actualizada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Medication>.MakeResponseFail(ex);
            }


        }

        public async Task<Response<Medication>> DeleteAsync(int id)
        {
            try
            {
                Medication? medication = await _context.Medications.FirstOrDefaultAsync(a => a.Id == id);
                if (medication is null)
                {
                    return ResponseHelper<Medication>.MakeResponseFail("El id indicado no existe");
                }
                _context.Medications.Remove(medication);
                await _context.SaveChangesAsync();
                return ResponseHelper<Medication>.MakeResponseSuccess(medication);


            }
            catch (Exception ex)
            {
                return ResponseHelper<Medication>.MakeResponseFail(ex);
            }
        }
    }
}