using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IAppoimentServices
    {
        public Task<Response<List<Appoiment>>> GetListAsync();
        public Task<Response<Appoiment>> CreateAsync(AppoimentDTO model);
        public Task<AppoimentDTO> CreateDTO();
        public Task<Response<AppoimentDTO>> GetOneAsync(int Id);
        public Task<Response<AppoimentDTO>> EditAsync(AppoimentDTO appoiment);
        public Task<Response<Appoiment>> DeleteAsync(int Id);

    }

    public class AppoimentServices : IAppoimentServices
    {
        private readonly DataContext _context;

        public AppoimentServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Appoiment>> CreateAsync(AppoimentDTO dto)
        {
            try { 
            Appoiment b = new Appoiment()
            {
                Date = dto.Date,
                Time = dto.Time,
                UserPatientId = dto.UserPatientId,
                UserDoctorId = dto.UserDoctorId
            };

            await _context.Appoiments.AddAsync(b);
            await _context.SaveChangesAsync();
            return ResponseHelper<Appoiment>.MakeResponseSuccess(b, "sección creada con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Appoiment>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Appoiment>>> GetListAsync()
        {
            try
            {
                List<Appoiment> appoiments = await _context.Appoiments.ToListAsync();
                return ResponseHelper<List<Appoiment>>.MakeResponseSuccess(appoiments);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Appoiment>>.MakeResponseFail(ex);
            }
        }

        public async Task<AppoimentDTO> CreateDTO()
        {
            AppoimentDTO dto = new AppoimentDTO
            {
                UserDoctor = await _context.Users.Select(a => new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                }).ToListAsync(),

                UserPatient = await _context.Users.Select(a => new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
            };
            return dto;
        }

        public async Task<Response<AppoimentDTO>> GetOneAsync(int id)
        {
            try
            {
                Appoiment? appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == id);

                if (appoiment is null)
                {
                    return ResponseHelper<AppoimentDTO>.MakeResponseFail("El id indicado no existe");
                }

                AppoimentDTO dto = new AppoimentDTO
                {
                    Date = appoiment.Date,
                    Time = appoiment.Time,
                   
                    UserDoctor = await _context.Users.Select(a => new SelectListItem
                    {
                        Text = $"{a.FirstName} {a.LastName}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),

                    UserPatient = await _context.Users.Select(a => new SelectListItem
                    {
                        Text = $"{a.FirstName} {a.LastName}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),
                };
                return ResponseHelper <AppoimentDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<AppoimentDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<AppoimentDTO>> EditAsync(AppoimentDTO appoiment)
        {
            try
            {

                Appoiment prueba = await _context.Appoiments.FirstOrDefaultAsync(u => u.Id == appoiment.Id);

                    prueba.Date = appoiment.Date;
                    prueba.Time = appoiment.Time;
                    prueba.UserDoctor = await _context.Users.FirstOrDefaultAsync(u => u.Id == appoiment.UserDoctorId);
                    prueba.UserPatient = await _context.Users.FirstOrDefaultAsync(u => u.Id == appoiment.UserPatientId);

                _context.Appoiments.Update(prueba);
                await _context.SaveChangesAsync();

                return ResponseHelper<AppoimentDTO>.MakeResponseSuccess(appoiment, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<AppoimentDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Appoiment>> DeleteAsync(int Id)
        {
            Appoiment? appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == Id);

            _context.Appoiments.Remove(appoiment);
            await _context.SaveChangesAsync();
            return ResponseHelper<Appoiment>.MakeResponseSuccess(appoiment, "sección actualizada con éxito");
  
        }


    }
}
