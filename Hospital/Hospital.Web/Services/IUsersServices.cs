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
    }

    public class UserServices : IUsersServices
    {
        private readonly DataContext _context;

        public UserServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<User>> CreateAsync(UserDTO dto)
        {
            try
            {
                User user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Birth = dto.Birth,
                    UserName = dto.UserName,
                    Password = dto.Password,
                    Rol = await _context.Roles.FirstOrDefaultAsync(a => a.Id == dto.RolId)

                };
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
                List<User> users = await _context.Users.ToListAsync();
                return ResponseHelper<List<User>>.MakeResponseSuccess(users);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<User>>.MakeResponseFail(ex);
            }
        }


    }
}
