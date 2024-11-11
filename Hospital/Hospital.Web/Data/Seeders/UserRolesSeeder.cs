using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
namespace Hospital.Web.Data.Seeders
{
    public class UserRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public UserRolesSeeder(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckUsers();
        }

        private async Task CheckUsers() //Creacion automatica de usuarios necesarios para la base de datos
        {
            // Usuario Admin
            User? user = await _usersService.GetUserAsync("DonNadie@yupimail.com");

            if (user is null)
            {
                HospitalRole adminRole = _context.HospitalRoles.FirstOrDefault(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                user = new User
                {
                    Email = "DonNadie@yupimail.com",
                    FirstName = "Don",
                    LastName = "Nadie",
                    PhoneNumber = "30000000",
                    UserName = "DonNadie@yupimail.com",
                    Document = "11111",
                    HospitalRole = adminRole
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }

            // Content Manager
            user = await _usersService.GetUserAsync("sinNombre@yupimail.com");

            if (user is null)
            {
                HospitalRole userManagerRole = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Gestor de usuarios");

                user = new User
                {
                    Email = "sinNombre@yupimail.com",
                    FirstName = "Sin",
                    LastName = "Nombre",
                    PhoneNumber = "31111111",
                    UserName = "sinNombre@yupimail.com",
                    Document = "22222",
                    HospitalRole = userManagerRole
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckRoles() //Creacion automatica de los roles necesarios para el sistema
        {
            await AdminRoleAsync();
            await ContentManagerAsync();
            await UserManagerAsync();
        }

        private async Task UserManagerAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Gestor de usuarios");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Gestor de usuarios" };
                await _context.HospitalRoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Usuarios").ToListAsync();
                
                foreach(Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task ContentManagerAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Gestor de medicamentos");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Gestor de medicamentos" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Medicamentos").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = Env.SUPER_ADMIN_ROLE_NAME };
                await _context.HospitalRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
