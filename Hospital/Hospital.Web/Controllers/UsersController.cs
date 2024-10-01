using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Web.Data;
using Microsoft.EntityFrameworkCore;



namespace Hospital.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServices _userService;


        public UsersController(IUsersServices userService)
        {
            _userService = userService;

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

                    return View(udto);
                }

                Response<User> response = await _userService.CreateAsync(udto);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                //TODO: MOstrar el mensaje  de error 
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
            //TODO: Mensaja de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TODO: mensaje de error
                    return View(section);
                }
                Response<UserDTO> response = await _userService.EditAsync(section);

                if (response.IsSuccess)
                {
                    //TODO: mensaje de exito
                    return RedirectToAction(nameof(Index));
                }
                //TODO: MOstrar el mensaje  de error 
                return View(response);
            }
            catch (Exception ex)
            {
                return View(section);
            }
        }
    }
}
