using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Web.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;



namespace Hospital.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServices _userService;

        private readonly INotyfService _notifyService;
        public UsersController(IUsersServices userService, INotyfService notifyService)
        {
            _userService = userService;
            _notifyService = notifyService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
                Response<List<User>> response = await _userService.GetListAsync();
                return View(response.Result);
            
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserDTO dto = await _userService.CreateDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO udto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(udto);
                }

                Response<User> response = await _userService.CreateAsync(udto);
                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha creado el Usuario con Èxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(udto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<UserDTO> response = await _userService.GetOneAsycn(id);
            if (response.IsSuccess)
            {

                return View(response.Result);
            }
            _notifyService.Error("Revise los datos ingresados por favor");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Revise los datos ingresados por favor");
                    return View(section);
                }
                Response<UserDTO> response = await _userService.EditAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Se ha actualizado con Èxito");
                    return RedirectToAction(nameof(Index));
                }
                _notifyService.Error("Revise los datos ingresados por favor");
                return View(response);
            }
            catch (Exception ex)
            {
                return View(section);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                _notifyService.Success("Se ha eliminado con Èxito");
                await _userService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
