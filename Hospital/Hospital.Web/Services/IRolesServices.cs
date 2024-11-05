using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IRolesServices
    {
        public Task<Response<PaginationResponse<HospitalRole>>> GetListAsync(PaginationRequest request);
        public Task<Response<HospitalRole>> GetOneAsync(int Id);
        public Task<Response<HospitalRole>> EditAsync(HospitalRole model);
        public Task<Response<HospitalRole>> CreateAsync(HospitalRole model);
        public Task<Response<HospitalRole>> DeleteAsync(int Id);

    }

    public class RolService : IRolesServices
    {
        private readonly DataContext _context;

        public RolService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<HospitalRole>> CreateAsync(HospitalRole model)
        {
            try
            {
                HospitalRole rol = new HospitalRole
                {
                    Name = model.Name
                };

                await _context.HospitalRoles.AddAsync(rol);
                await _context.SaveChangesAsync();

                return ResponseHelper<HospitalRole>.MakeResponseSuccess(rol, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<HospitalRole>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<HospitalRole>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<HospitalRole> query = _context.HospitalRoles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(r => r.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<HospitalRole> list = await PagedList<HospitalRole>.ToPagedListAsync(query, request);

                PaginationResponse<HospitalRole> result = new PaginationResponse<HospitalRole>
                {
                    List = list,
                    TotalCount = list.Count,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<HospitalRole>>.MakeResponseSuccess(result, "Roles obtenidos con exito");

            }
            catch (Exception ex) { 
                return ResponseHelper<PaginationResponse<HospitalRole>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<HospitalRole>> GetOneAsync(int Id)
        {
            try
            {
                HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(r => r.Id == Id);

                if (rol is null) {
                    return ResponseHelper<HospitalRole>.MakeResponseFail("El id indicado no existe");
                }

                
                return ResponseHelper<HospitalRole>.MakeResponseSuccess(rol);
                
            }
            catch (Exception ex)
            {
                return ResponseHelper<HospitalRole>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<HospitalRole>> EditAsync(HospitalRole model)
        {
            try
            {
                _context.HospitalRoles.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<HospitalRole>.MakeResponseSuccess(model, "seccion actualizada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<HospitalRole>.MakeResponseFail(ex);
            }


        }

        public async Task<Response<HospitalRole>> DeleteAsync(int Id)
        {
            HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(a => a.Id == Id);
            _context.HospitalRoles.Remove(rol);
            await _context.SaveChangesAsync();
            return ResponseHelper<HospitalRole>.MakeResponseSuccess(rol, "sección actualizada con éxito");
        }

    }
}
