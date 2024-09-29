using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IAppoimentServices
    {
        public Task<Response<List<Appoiment>>> GetListAsync();
        public Task<Response<Appoiment>> CreateAsync(AppoimentDTO model);

        public Task<AppoimentDTO> GetOneAsync();

    }

    public class AppoimentServices : IAppoimentServices
    {
        private readonly DataContext _context;

        public AppoimentServices(DataContext context)
        {
            _context = context;
        }

        public Task<Response<Appoiment>> CreateAsync(AppoimentDTO model)
        {
            throw new NotImplementedException();
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

        public async Task<AppoimentDTO> GetOneAsync()
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


    }
}
