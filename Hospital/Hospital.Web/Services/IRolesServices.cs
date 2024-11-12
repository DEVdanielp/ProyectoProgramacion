using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Data;


namespace Hospital.Web.Services
{
    public interface IRolesServices
    {
        public Task<Response<PaginationResponse<HospitalRole>>> GetListAsync(PaginationRequest request);
        public Task<Response<HospitalRoleDTO>> GetOneAsync(int Id);
        //public Task<Response<HospitalRole>> EditAsync(HospitalRole model);
        public Task<Response<HospitalRole>> CreateAsync(HospitalRoleDTO dto);
        //public Task<Response<HospitalRole>> DeleteAsync(int Id);
        public Task<Response<IEnumerable<Permission>>> GetPermissionsAsync();
    }

    public class RolServices : IRolesServices
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RolServices(DataContext context)
        {
            _context = context;
        }
        public async Task<Response<HospitalRole>> CreateAsync(HospitalRoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // creación del rol
                    HospitalRole role = _converterHelper.ToRole(dto);
                    await _context.HospitalRoles.AddAsync(role);

                    await _context.SaveChangesAsync();
                    //insercion de permisos
                    int roleId = role.Id;

                    List<int> permissionsIds = new List<int>();
                    if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                    {
                        permissionsIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    }

                    foreach (int permissionsId in permissionsIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionsId,
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                    }
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return ResponseHelper<HospitalRole>.MakeResponseSuccess(role, "Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResponseHelper<HospitalRole>.MakeResponseFail(ex);
                }
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
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<HospitalRole>>.MakeResponseFail(ex);
            }
        }







        //public async Task<Response<HospitalRole>> EditAsync(HospitalRole model)
        //{
        //    try
        //    {
        //        _context.HospitalRoles.Update(model);
        //        await _context.SaveChangesAsync();

        //        return ResponseHelper<HospitalRole>.MakeResponseSuccess(model, "seccion actualizada con exito");

        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseHelper<HospitalRole>.MakeResponseFail(ex);
        //    }


        //}

        //public async Task<Response<HospitalRole>> DeleteAsync(int Id)
        //{
        //    HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(a => a.Id == Id);
        //    _context.HospitalRoles.Remove(rol);
        //    await _context.SaveChangesAsync();
        //    return ResponseHelper<HospitalRole>.MakeResponseSuccess(rol, "sección actualizada con éxito");
        //}

        //public async Task<Response<HospitalRoleDTO>> GetOneAsync(int Id)
        //{
        //    try
        //    {
        //        HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(r => r.Id == Id);

        //        if (rol is null)
        //        {
        //            return ResponseHelper<HospitalRoleDTO>.MakeResponseFail("El id indicado no existe");
        //        }


        //        return ResponseHelper<HospitalRoleDTO>.MakeResponseSuccess(await _converterHelper.ToRoleDTOAsync(rol));

        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseHelper<HospitalRoleDTO>.MakeResponseFail(ex);
        //    }
        //}





        public Task<Response<HospitalRoleDTO>> GetOneAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<Permission>>> GetPermissionsAsync()
        {
            try
            {
                IEnumerable<Permission> permissions = await _context.Permissions.ToListAsync();

                return ResponseHelper<IEnumerable<Permission>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<Permission>>.MakeResponseFail(ex);
            }

        }
    }
}
