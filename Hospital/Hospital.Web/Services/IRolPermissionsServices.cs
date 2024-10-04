using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IRolPermissionsServices
    {
        public Task<Response<List<RolesPermission>>> GetListAsync();
        public Task<Response<RolesPermission>> CreateAsync(RolPermissionsDTO model);
        public Task<RolPermissionsDTO> CreateDTO();
        //public Task<Response<RolesPermission>> DeleteAsync(int Id);
    }

    public class RolPermissionServices : IRolPermissionsServices
    {
        private readonly DataContext _context;

        public RolPermissionServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<RolesPermission>> CreateAsync(RolPermissionsDTO dto)
        {
            try
            {
                RolesPermission rp = new RolesPermission()
                {
                    PermisosId = dto.PermisosId,
                    rolId = dto.PermisosId
                };

                await _context.RolesPermisos.AddAsync(rp);
                await _context.SaveChangesAsync();
                return ResponseHelper<RolesPermission>.MakeResponseSuccess(rp, "sección creada con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<RolesPermission>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<RolesPermission>>> GetListAsync()
        {
            try
            {
                List<RolesPermission> rp = await _context.RolesPermisos.Include(u => u.Rol).Include(o => o.Permisos).ToListAsync();
                return ResponseHelper<List<RolesPermission>>.MakeResponseSuccess(rp);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<RolesPermission>>.MakeResponseFail(ex);
            }
        }

        public async Task<RolPermissionsDTO> CreateDTO()
        {
            RolPermissionsDTO dto = new RolPermissionsDTO
            {
                Rol = await _context.Roles.Select(a => new SelectListItem
                {
                    Text = $"{a.NameRol}",
                    Value = a.Id.ToString()
                }).ToListAsync() ,              
                
                Permisos = await _context.Permissions.Select(a => new SelectListItem
                {
                    Text = $"{a.Name}",
                    Value = a.Id.ToString()
                }).ToListAsync()
            };
            return dto;
        }

       //// public async Task<Response<Appoiment>> DeleteAsync(int Id)
       // {
       //     Appoiment? appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == Id);

       //     _context.Appoiments.Remove(appoiment);
       //     await _context.SaveChangesAsync();
       //     return ResponseHelper<Appoiment>.MakeResponseSuccess(appoiment, "sección actualizada con éxito");

       // }


    }
}


