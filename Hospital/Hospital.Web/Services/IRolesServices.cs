using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IRolesServices
    {
        public Task<Response<List<Rol>>> GetListAsync();
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
                    NameRol = model.NameRol,
                    Description = model.Description
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

        public async Task<Response<List<Rol>>> GetListAsync()
        {
            try
            {
                List<Rol> roles = await _context.Roles.ToListAsync();
                return ResponseHelper<List<Rol>>.MakeResponseSuccess(roles);
            }
            catch (Exception ex) { 
                return ResponseHelper<List<Rol>>.MakeResponseFail(ex);
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
