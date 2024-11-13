using AspNetCoreHero.ToastNotification.Abstractions;

using Hospital.Web.Core;
using Hospital.Web.Core.Attributes;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hospital.Web.Controllers
{
    public class RolesController : Controller
    {
        //Inyectamos la dependecia de la interfaz creada
        private readonly IRolesServices _rolesService;
        private readonly INotyfService _notifyService;

        public RolesController(IRolesServices rolesService, INotyfService notifyService)
        {
            _rolesService = rolesService;
            _notifyService = notifyService;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showRoles", module: "Roles")]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? page,
                                               [FromQuery] string? filter)
        {
            PaginationRequest request = new PaginationRequest()
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = page ?? 1,
                Filter = filter
            };

            Response<PaginationResponse<HospitalRole>> response = await _rolesService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            Response<IEnumerable<Permission>> response = await _rolesService.GetPermissionsAsync();
            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }
            HospitalRoleDTO dto = new HospitalRoleDTO
            {
                // ese select es una lista, sirve tanto para generar querys o formatear listas estáticas
                Permissions = response.Result.Select(p => new PermissionForDTO

                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,

                }).ToList()
            };

            return View(dto);
        }

        [HttpPost]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(HospitalRoleDTO dto)
        {
         
           
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");

                    Response<IEnumerable<Permission>> response1 = await _rolesService.GetPermissionsAsync();

                    dto.Permissions = response1.Result.Select(p => new PermissionForDTO

                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Module = p.Module,

                        }).ToList();
                    return View(dto);
                }
                Response<HospitalRole> createResponse = await _rolesService.CreateAsync(dto);

                if (!createResponse.IsSuccess)
                {
                    _notifyService.Success(createResponse.Message);
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error(createResponse.Message);

                Response<IEnumerable<Permission>> response = await _rolesService.GetPermissionsAsync();

                dto.Permissions = response.Result.Select(p => new PermissionForDTO

                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,

                }).ToList();
                return View(dto);
            }

            }
    }


        //[HttpGet]
        //public async Task<IActionResult> Edit([FromRoute] int Id)
        //{

        //    Response<HospitalRole> response = await _rolesService.GetOneAsync(Id);

        //    if (response.IsSuccess)
        //    {
        //        return View(response.Result);
        //    }

        //    _notifyService.Error("Revise los datos ingresados por favor");
        //    return RedirectToAction(nameof(Index));

        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(HospitalRole rol)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            _notifyService.Error("Revise los datos ingresados por favor");
        //            return View(rol);
        //        }

        //        Response<HospitalRole> response = await _rolesService.EditAsync(rol);

        //        if (response.IsSuccess)
        //        {
        //            _notifyService.Success("Se ha actualizado este rol con éxito");
        //            return RedirectToAction(nameof(Index));
        //        }

        //        _notifyService.Error("Revise los datos ingresados por favor");
        //        return View(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(rol);
        //    }
        //}


        //[HttpPost]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{   //Este metodo redirecciona confirma la eliminacion
        //    try
        //    {
        //        await _rolesService.DeleteAsync(id);
        //        _notifyService.Success("Se ha eliminado con éxito");
        //        return RedirectToAction(nameof(Index));

        //    }
        //    catch
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //}


//    }
//}
