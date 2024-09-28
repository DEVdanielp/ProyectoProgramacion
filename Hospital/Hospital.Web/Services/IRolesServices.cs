using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IRolesServices
    {
        public Task<Response<List<Rol>>> GetListAsync();
    }

    public class RolService : IRolesServices
    {
        private readonly DataContext _context;

        public RolService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Rol>>> GetListAsync()
        {
            try
            {
                List<Rol> roles = await _context.Roles.ToListAsync();
                return ResponseHelper<List<Rol>>.MakeResponseSuccess(roles);
            }
            catch (Exception ex) { 
                return ResponseHelper<List<Rol>>.MakeResponseFail(ex);
            }
        }
    }
}
