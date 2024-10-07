using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;




namespace Hospital.Web.Services
{
    public interface IUsersServices
    {
        public Task<Response<User>> CreateAsync(UserDTO dto);
        public Task<Response<List<User>>> GetListAsync();
        public Task<Response<UserDTO>> EditAsync(UserDTO dto);
        public Task<Response<UserDTO>> GetOneAsycn(int id);
        public Task<Response<User>> DeleteAsync(int Id);
    }

    public class UserServices : IUsersServices
    {
        private readonly DataContext _context;

        private readonly IConvertHelper _convertHelper;
        public UserServices(DataContext context, IConvertHelper convertHelper)
        {
            _context = context;
            _convertHelper = convertHelper;
        }

        public async Task<Response<User>> CreateAsync(UserDTO dto)
        {
            try
            {
                User user = _convertHelper.ToUser(dto);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return ResponseHelper<User>.MakeResponseSuccess(user, "Seccion creada con exito ");

            }
            catch (Exception ex)
            {
                return ResponseHelper<User>.MakeResponseFail(ex);
            }
        }
       
        public async Task<Response<List<User>>> GetListAsync()
        {
            try
            {
                List<User> users = await _context.Users.Include(u => u.Rol).ToListAsync();
                return ResponseHelper<List<User>>.MakeResponseSuccess(users);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<User>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<UserDTO>> EditAsync(UserDTO dto)
        {
            try
            {

                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);

                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    user.Birth = dto.Birth;
                    user.UserName = dto.UserName;
                    user.Password = dto.Password;
                    user.Rol = await _context.Roles.FirstOrDefaultAsync(a => a.Id == dto.RolId);

            

                   
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return ResponseHelper<UserDTO>.MakeResponseSuccess(dto, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<UserDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<UserDTO>> GetOneAsycn(int id)
        {
            try
            {
                User? user = await _context.Users.Include(b => b.Rol).FirstOrDefaultAsync(a => a.Id == id);
                if (user == null)
                {
                    return ResponseHelper<UserDTO>.MakeResponseFail("En la seccion con el id indicado no existe");
                }
                UserDTO dto = new UserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birth = user.Birth,
                    UserName = user.UserName,
                    Password = user.Password,
                    Rols = await _context.Roles.Select(a => new SelectListItem
                    {
                        Text = $"{a.NameRol}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),

                };
                return ResponseHelper<UserDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<UserDTO>.MakeResponseFail(ex);
            } 
        }

        public async Task<Response<User>> DeleteAsync(int Id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(a => a.Id == Id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return ResponseHelper<User>.MakeResponseSuccess(user, "sección actualizada con éxito");

        }
    }
}
