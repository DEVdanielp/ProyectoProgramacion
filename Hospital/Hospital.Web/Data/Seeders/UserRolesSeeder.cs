using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using System.Reflection;
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

            //Administrador
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
            //Final administrador

            //Gestor medicamentos
            user = await _usersService.GetUserAsync("medicamentos@yupimail.com");

            if (user is null)
            {
                HospitalRole medicamentos = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Gestor de Medicamentos");

                user = new User
                {
                    Email = "JuanPerez@yupimail.com",
                    FirstName = "Juan",
                    LastName = "Perez",
                    PhoneNumber = "32222222",
                    UserName = "JuanPerez@yupimail.com",
                    Document = "33333",
                    HospitalRole = medicamentos
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
            //Final gestor medicamentos

            //Gestor de usuarios
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
            //Final gestor de usuarios

            //Doctor
            user = await _usersService.GetUserAsync("Doctor@yupimail.com");

            if (user is null)
            {
                HospitalRole doctor = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Doctor");

                user = new User
                {
                    Email = "Doctor@yupimail.com",
                    FirstName = "Doctor",
                    LastName = "Atiende",
                    PhoneNumber = "32222222",
                    UserName = "Doctor@yupimail.com",
                    Document = "33333",
                    HospitalRole = doctor
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
            //Final Doctor

            //Paciente 1
            user = await _usersService.GetUserAsync("paciente1@yupimail.com");

            if (user is null)
            {
                HospitalRole medicamentos = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Paciente");

                user = new User
                {
                    Email = "paciente@yupimail.com",
                    FirstName = "Paciente",
                    LastName = "Primero",
                    PhoneNumber = "32222222",
                    UserName = "paciente@yupimail.com",
                    Document = "101010",
                    HospitalRole = medicamentos
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
            //Final paciente 1

            //Paciente 2

            user = await _usersService.GetUserAsync("paciente2@yupimail.com");

            if (user is null)
            {
                HospitalRole medicamentos = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Paciente");

                user = new User
                {
                    Email = "paciente2@yupimail.com",
                    FirstName = "Paciente",
                    LastName = "Segundo",
                    PhoneNumber = "32222222",
                    UserName = "paciente2@yupimail.com",
                    Document = "202020",
                    HospitalRole = medicamentos
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }

            //Final paciente 2

            //Recepcionista

            user = await _usersService.GetUserAsync("recepcionista@yupimail.com");

            if (user is null)
            {
                HospitalRole medicamentos = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Recepcionista");

                user = new User
                {
                    Email = "recepcionista@yupimail.com",
                    FirstName = "Recepcionista",
                    LastName = "Recepciona",
                    PhoneNumber = "32222222",
                    UserName = "recepcionista@yupimail.com",
                    Document = "303030",
                    HospitalRole = medicamentos
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
            //Final recepcionista

            //Farmaceutico
            user = await _usersService.GetUserAsync("farmaceutico@yupimail.com");

            if (user is null)
            {
                HospitalRole medicamentos = _context.HospitalRoles.FirstOrDefault(r => r.Name == "Farmaceutico");

                user = new User
                {
                    Email = "farmaceutico@yupimail.com",
                    FirstName = "Farmacia",
                    LastName = "Farmaceutica",
                    PhoneNumber = "32222222",
                    UserName = "farmaceutico@yupimail.com",
                    Document = "404040",
                    HospitalRole = medicamentos
                };

                await _usersService.AddUserAsync(user, "1234");

                string token = await _usersService.GenerateEmailConfirmationTokenAsync(user);
                await _usersService.ConfirmEmailAsync(user, token);
            }
            //Final farmaceutico
        }

        private async Task CheckRoles() //Creacion automatica de los roles necesarios para el sistema
        {
            await AdminRoleAsync();
            await GestorMedicamentosAsync();
            await UserManagerAsync();
            await DoctorAsync();
            await PatientAsync();
            await RecepcionistaAsync();
            await FarmaceuticoAsync();
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

        private async Task GestorMedicamentosAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Gestor de medicamentos");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Gestor de medicamentos" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Medicamentos" || p.Module== "Órdenes Médicas").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task DoctorAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Doctor");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Doctor" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Especialidades Medicas" || p.Module == "Órdenes Médicas" || p.Module == "Citas" || p.Module == "Estado" || p.Module == "Medicamentos" || p.Module == "Historia Clínica").ToListAsync();

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

        private async Task PatientAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Paciente");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Paciente" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Citas" || p.Module == "Medicamentos" || p.Module == "Historia Clínica").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task RecepcionistaAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Recepcionista");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Recepcionista" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Órdenes Médicas" || p.Module == "Citas" || p.Module == "Estado" || p.Module == "Medicamentos" || p.Module == "Historia Clínica").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task FarmaceuticoAsync()
        {
            bool exists = await _context.HospitalRoles.AnyAsync(r => r.Name == "Farmaceutico");

            if (!exists)
            {
                HospitalRole role = new HospitalRole { Name = "Farmaceutico" };
                await _context.HospitalRoles.AddAsync(role);
                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Medicamentos" || p.Module == "Orden Medica").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }
                await _context.SaveChangesAsync();
            }
        }


    }


}

