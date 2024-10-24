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
        public Task<Response<PaginationResponse<Rol>>> GetListAsync(PaginationRequest request);
        public Task<Response<Rol>> GetOneAsync(int Id);
        public Task<Response<Rol>> EditAsync(Rol model);
        public Task<Response<Rol>> CreateAsync(Rol model);
        public Task<Response<Rol>> DeleteAsync(int Id);

    }

    public class RolService : IRolesServices
    {
        private readonly DataContext _context;

        public RolService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Rol>> CreateAsync(Rol model)
        {
            try
            {
                Rol rol = new Rol
                {
                    NameRol = model.NameRol
                };

                await _context.Roles.AddAsync(rol);
                await _context.SaveChangesAsync();

                return ResponseHelper<Rol>.MakeResponseSuccess(rol, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Rol>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Rol>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Rol> query = _context.Roles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(r => r.NameRol.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Rol> list = await PagedList<Rol>.ToPagedListAsync(query, request);

                PaginationResponse<Rol> result = new PaginationResponse<Rol>
                {
                    List = list,
                    TotalCount = list.Count,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<Rol>>.MakeResponseSuccess(result, "Roles obtenidos con exito");

            }
            catch (Exception ex) { 
                return ResponseHelper<PaginationResponse<Rol>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Rol>> GetOneAsync(int Id)
        {
            try
            {
                Rol? rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == Id);

                if (rol is null) {
                    return ResponseHelper<Rol>.MakeResponseFail("El id indicado no existe");
                }

                
                return ResponseHelper<Rol>.MakeResponseSuccess(rol);
                
            }
            catch (Exception ex)
            {
                return ResponseHelper<Rol>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Rol>> EditAsync(Rol model)
        {
            try
            {
                _context.Roles.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Rol>.MakeResponseSuccess(model, "seccion actualizada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Rol>.MakeResponseFail(ex);
            }


        }

        public async Task<Response<Rol>> DeleteAsync(int Id)
        {
            Rol? rol = await _context.Roles.FirstOrDefaultAsync(a => a.Id == Id);
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return ResponseHelper<Rol>.MakeResponseSuccess(rol, "sección actualizada con éxito");
        }

    }
}
