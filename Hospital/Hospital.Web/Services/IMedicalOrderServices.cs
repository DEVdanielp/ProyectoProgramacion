using Hospital.Web.Data.Entities;
using Hospital.Web.Data;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;

namespace Hospital.Web.Services
{
    public interface IMedicalOrdersServices
    {
        public Task<Response<List<MedicalOrder>>> GetListAsync();
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

        public MedicalOrdersServices(DataContext context)
        {
            _context = context;
        }


        public async Task<Response<MedicalOrderDTO>> CreateAsync(MedicalOrderDTO medicalOrderdto)
        {
            try
            {
                MedicalOrder medicalorder = new MedicalOrder
                {
                    Diagnosis = medicalOrderdto.Diagnosis,
                    Description = medicalOrderdto.Description,
                    Appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == medicalOrderdto.AppoimentId),
                    Medication = await _context.Medications.FirstOrDefaultAsync(a => a.Id == medicalOrderdto.MedicationId)
                };
                await _context.MedicalOrders.AddAsync(medicalorder);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalOrderDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<MedicalOrder>>> GetListAsync()
        {
            try
            {
                List<MedicalOrder> medicalOrders = await _context.MedicalOrders.ToListAsync();
                return ResponseHelper<List<MedicalOrder>>.MakeResponseSuccess(medicalOrders);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<MedicalOrder>>.MakeResponseFail(ex);
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

                Medications = await _context.Medications.Select(a => new SelectListItem
                {
                    Text = $"{a.CommercialName} {a.Description}",
                    Value = a.Id.ToString()
                }).ToListAsync(),

                Appoiments = await _context.Appoiments.Select(a => new SelectListItem
                {
                    Text = $"Fecha:{a.Date}, Hora:{a.Time}, Doctor: {a.UserDoctorId}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
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
                    Text = $"Fecha:{a.Date}, Hora:{a.Time}, Doctor: {a.UserDoctorId}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
            };
            return ResponseHelper<MedicalOrderDTO>.MakeResponseSuccess(medicalOrderdto);
        }
    }
}


