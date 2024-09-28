using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Hospital.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> list = await _context.Users.Include(b => b.Rol).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserDTO udto = new UserDTO
            {
                Rol = await _context.Roles.Select(a => new SelectListItem
                {
                    Text = $"{a.NameRol} {a.Description}",
                    Value = a.NameRol.ToString()
                }).ToListAsync(),
            };
            return View(udto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }
                User user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Birth = dto.Birth,
                    UserName = dto.UserName,
                    Password = dto.Password,
                    Rol = await _context.Roles.FirstOrDefaultAsync(a => a.Id == dto.RolId),
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                User? user = await _context.Users.Include(b => b.Rol).FirstOrDefaultAsync(a => a.Id == id);

                UserDTO dto = new UserDTO
                {
                    Id = id,
                    RolId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birth = user.Birth,
                    UserName = user.UserName,
                    Password = user.Password,

                    Rol = await _context.Roles.Select(a => new SelectListItem
                    {
                        Text = $"{a.NameRol} {a.Description}",
                        Value = a.NameRol.ToString()
                    }).ToListAsync(),
                };

                return View(dto);

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    dto.Rol = await _context.Roles.Select(a => new SelectListItem
                    {
                        Text = $"{a.NameRol} {a.Description}",
                        Value = a.NameRol.ToString()
                    }).ToArrayAsync();
                    return View(dto);

                }

                User user = await _context.Users.FirstOrDefaultAsync(a => a.Id == dto.Id);

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Birth = dto.Birth;
                user.UserName = dto.UserName;
                user.Password = dto.Password;
                user.Rol = await _context.Roles.FirstOrDefaultAsync(a => a.Id == dto.RolId);


                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
