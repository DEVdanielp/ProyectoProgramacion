using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;
using Hospital.Web.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Hospital.Web.Helpers;
using Hospital.Web.Core.Pagination;
using Microsoft.AspNetCore.Authorization;
using Hospital.Web.Core.Attributes;



namespace Hospital.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ICombosHelpers _combosHelper;
        private readonly INotyfService _notifyService;
        private readonly IUsersService _usersService;
        private readonly IConverterHelper _converterHelper;

        public UsersController(ICombosHelpers combosHelper, INotyfService notifyService, IUsersService usersService, IConverterHelper converterHelper)
        {
            _combosHelper = combosHelper;
            _notifyService = notifyService;
            _usersService = usersService;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showUser", module: "Usuarios")]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };

            Response<PaginationResponse<User>> response = await _usersService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createUser", module: "Usuarios")]
        public async Task<IActionResult> Create()
        {
            UserDTO dto = new UserDTO
            {
                HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync(),
            };

            return View(dto);
        }

        [HttpPost]
        [CustomAuthorize(permission: "createUser", module: "Usuarios")]
        public async Task<IActionResult> Create(UserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    dto.HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync();
                    return View(dto);
                }

                Response<User> response = await _usersService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                dto.HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync();
                return View(dto);
            }
            catch (Exception ex)
            {
                dto.HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync();
                return View(dto);
            }
        }


        [HttpGet]
        [CustomAuthorize(permission: "updateUser", module: "Usuarios")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                return NotFound();
            }

            User user = await _usersService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            UserDTO dto = await _converterHelper.ToUserDTOAsync(user, false);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(permission: "updateUser", module: "Usuarios")]
        public async Task<IActionResult> Edit(UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync();
                return View(dto);
            }

            Response<User> response = await _usersService.UpdateUserAsync(dto);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(response.Message);
            dto.HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync();
            return View(dto);
        }
    }
}
