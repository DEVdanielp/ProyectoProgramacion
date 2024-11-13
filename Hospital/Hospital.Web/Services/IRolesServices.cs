using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public Task<Response<HospitalRole>> EditAsync(HospitalRoleDTO dto);
        public Task<Response<HospitalRole>> CreateAsync(HospitalRoleDTO dto);
        public Task<Response<HospitalRole>> DeleteAsync(int Id, HospitalRoleDTO dto);
        public Task<Response<IEnumerable<Permission>>> GetPermissionsAsync();
        Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id);
    }

    public class RolServices : IRolesServices
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RolServices(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
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
                            RoleId = roleId,
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

        public async Task<Response<HospitalRole>> EditAsync(HospitalRoleDTO dto)
        {
            
                try
                {
                    if (dto.Name == Env.SUPER_ADMIN_ROLE_NAME)
                    {
                    return ResponseHelper<HospitalRole>.MakeResponseFail($"El role '{Env.SUPER_ADMIN_ROLE_NAME}' no puede ser editado");
                    };
                    List<int> permissionIds = new List<int>();
                        if(!string.IsNullOrWhiteSpace(dto.PermissionIds))
                          {
                                permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);

                          }
                        //Eliminacion de permisos antiguos
                        List<RolePermission> oldrolePermissions = await _context.RolePermissions.Where(rp => rp.RoleId == dto.Id).ToListAsync();
                        _context.RolePermissions.RemoveRange(oldrolePermissions);

                //Insercion de nuevos permisos
                foreach (int permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = dto.Id,
                        PermissionId = permissionId
                    };
                    await _context.RolePermissions.AddAsync(rolePermission);
                }

                // actualizacion de rol
                HospitalRole model = _converterHelper.ToRole(dto);

                _context.HospitalRoles.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<HospitalRole>.MakeResponseSuccess(model, "Rol actualizado con éxito");
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
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<HospitalRole>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<HospitalRole>> DeleteAsync(int Id, HospitalRoleDTO dto)
        {

            HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(a => a.Id == Id);
            List<RolePermission> oldrolePermissions = await _context.RolePermissions.Where(rp => rp.RoleId == dto.Id).ToListAsync();
            _context.RolePermissions.RemoveRange(oldrolePermissions);
            _context.HospitalRoles.Remove(rol);
            await _context.SaveChangesAsync();
            return ResponseHelper<HospitalRole>.MakeResponseSuccess(rol, "sección actualizada con éxito");
        }

        public async Task<Response<HospitalRoleDTO>> GetOneAsync(int Id)
        {
            try
            {
                HospitalRole? rol = await _context.HospitalRoles.FirstOrDefaultAsync(r => r.Id == Id);

                if (rol is null)
                {
                    return ResponseHelper<HospitalRoleDTO>.MakeResponseFail("El id indicado no existe");
                }


                return ResponseHelper<HospitalRoleDTO>.MakeResponseSuccess(await _converterHelper.ToRoleDTOAsync(rol));

            }
            catch (Exception ex)
            {
                return ResponseHelper<HospitalRoleDTO>.MakeResponseFail(ex);
            }
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

        public async Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id)
        {
            try
            {
                Response<HospitalRoleDTO> response = await GetOneAsync(id);
                if( !response.IsSuccess )
                {
                    return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseFail(response.Message);

                }
                List<PermissionForDTO> permissions = response.Result.Permissions;

                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseSuccess(permissions);
            }

            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseFail(ex);
            }
        }
    }
}
