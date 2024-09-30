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
        private readonly DataContext _context;

        public UsersController(IUsersServices userService, DataContext context)
        {
            _userService = userService;
            _context = context;

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

            UserDTO udto = new UserDTO
            {
                Rols = await _context.Roles.Select(a => new SelectListItem
                {
                    Text = $"{a.NameRol}",
                    Value = a.Id.ToString()
                }).ToListAsync(),
            };
            return View();
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

    }
}
