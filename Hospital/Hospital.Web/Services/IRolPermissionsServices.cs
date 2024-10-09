using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Hospital.Web.Services
{
    public interface IRolPermissionsServices
    {
        public Task<Response<List<RolesPermission>>> GetListAsync();
        public Task<Response<RolesPermission>> CreateAsync(RolPermissionsDTO model);
        public Task<Response<RolesPermission>> DeleteAsync(int Idp, int Idr);
    }

    public class RolPermissionServices : IRolPermissionsServices
    {
        private readonly DataContext _context;
        private readonly IConvertHelper _converter;
        private readonly ICombosHelpers _combosHelpers;
        public RolPermissionServices(DataContext context, IConvertHelper converter, ICombosHelpers combosHelpers)
        {
            _context = context;
            _converter = converter;
            _combosHelpers = combosHelpers;
        }

        public async Task<Response<RolesPermission>> CreateAsync(RolPermissionsDTO dto)
        {
            try
            {
                RolesPermission rp = _converter.ToRP(dto);
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

        public async Task<Response<RolesPermission>> DeleteAsync(int Idp, int Idr)
        {
            RolesPermission? rp = await _context.RolesPermisos.FirstOrDefaultAsync(a => a.PermisosId == Idp && a.rolId == Idr);

            _context.RolesPermisos.Remove(rp);
            await _context.SaveChangesAsync();
            return ResponseHelper<RolesPermission>.MakeResponseSuccess(rp, "sección actualizada con éxito");

        }


    }
}


