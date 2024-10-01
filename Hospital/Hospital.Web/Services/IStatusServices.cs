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
    public interface IStatusServices
    {
        public Task<Response<List<Status>>> GetListAsync();
        public Task<Response<Status>> CreateAsync(StatusDTO model);
        public Task<StatusDTO> CreateDTO();
        public Task<Response<StatusDTO>> GetOneAsync(int Id);
        public Task<Response<StatusDTO>> EditAsync(StatusDTO status);
        public Task<Response<Status>> DeleteAsync(int Id);

    }

    public class StatusServices : IStatusServices
    {
        private readonly DataContext _context;

        public StatusServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Status>> CreateAsync(StatusDTO dto)
        {
            try
            {
                Status b = new Status()
                {
                    StatusAppoiment = dto.StatusAppoiment,
                    AppoimentId = dto.AppoimentId
                };

                await _context.Status.AddAsync(b);
                await _context.SaveChangesAsync();
                return ResponseHelper<Status>.MakeResponseSuccess(b, "sección creada con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Status>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Status>>> GetListAsync()
        {
            try
            {
                List<Status> status = await _context.Status.ToListAsync();
                return ResponseHelper<List<Status>>.MakeResponseSuccess(status);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Status>>.MakeResponseFail(ex);
            }
        }

        public async Task<StatusDTO> CreateDTO()
        {
            StatusDTO dto = new StatusDTO
            {
                Appoiment = await _context.Appoiments.Select(a => new SelectListItem
                {
                    Text = $"Fecha: {a.Date}, Hora: {a.Time}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
            };
            return dto;
        }

        public async Task<Response<StatusDTO>> GetOneAsync(int id)
        {
            try
            {
                Status? status = await _context.Status.FirstOrDefaultAsync(a => a.Id == id);

                if (status is null)
                {
                    return ResponseHelper<StatusDTO>.MakeResponseFail("El id indicado no existe");
                }

                StatusDTO dto = new StatusDTO
                {
                    StatusAppoiment = status.StatusAppoiment,

                    Appoiment = await _context.Appoiments.Select(a => new SelectListItem
                    {
                        Text = $"Fecha: {a.Date}, Hora: {a.Time}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),

                };
                return ResponseHelper<StatusDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<StatusDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<StatusDTO>> EditAsync(StatusDTO status)
        {
            try
            {
                Console.WriteLine(status.AppoimentId);
                Status prueba = await _context.Status.FirstOrDefaultAsync(u => u.Id == status.Id);
                
                prueba.StatusAppoiment = status.StatusAppoiment;
                prueba.Appoiment = await _context.Appoiments.FirstOrDefaultAsync(u => u.Id == status.AppoimentId);

                _context.Status.Update(prueba);
                await _context.SaveChangesAsync();

                return ResponseHelper<StatusDTO>.MakeResponseSuccess(status, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<StatusDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Status>> DeleteAsync(int Id)
        {
            Status? status = await _context.Status.FirstOrDefaultAsync(a => a.Id == Id);

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();
            return ResponseHelper<Status>.MakeResponseSuccess(status, "sección actualizada con éxito");

        }
    }
}
