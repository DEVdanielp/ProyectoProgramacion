using Hospital.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Permission> permissions = [.. MedicalSpes(), .. Medications(), .. Users(), .. MedicalOrderPermissions(), .. MedicalHistoryPermissions(), .. RolesPermissions()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name && p.Module == permission.Module);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();
        }

        private List<Permission> MedicalSpes()
        {
            return new List<Permission>
            {
                new Permission { Name = "showMedicalSpecialization", Description = "Ver Especialidades Medicas", Module = "Especialidades Medicas" },
                new Permission { Name = "createMedicalSpecialization", Description = "Crear Especialidades Medicas", Module = "Especialidades Medicas" },
                new Permission { Name = "updateMedicalSpecialization", Description = "Editar Especialidades Medicas", Module = "Especialidades Medicas" },
                new Permission { Name = "deleteMedicalSpecialization", Description = "Eliminar Especialidades Medicas", Module = "Especialidades Medicas" },
            };
        }

        private List<Permission> Appoiments()
        {
            return new List<Permission>
            {
                new Permission { Name = "showAppoiment", Description = "Ver Citas", Module = "Citas" },
                new Permission { Name = "createAppoiment", Description = "Crear Citas", Module = "Citas" },
                new Permission { Name = "updateAppoiment", Description = "Editar Citas", Module = "Citas" },
                new Permission { Name = "deleteAppoiment", Description = "Eliminar Citas", Module = "Citas" },
            };
        }

        private List<Permission> Status()
        {
            return new List<Permission>
            {
                new Permission { Name = "showStatu", Description = "Ver Estado de la citas", Module = "Estado" },
                new Permission { Name = "createStatu", Description = "Crear Estado de la citas", Module = "Estado" },
                new Permission { Name = "updateStatu", Description = "Editar Estado de la citas", Module = "Estado" },
                new Permission { Name = "deleteStatu", Description = "Eliminar Estado de la citas", Module = "Estado" },
            };
        }


        private List<Permission> Medications()
        {
            return new List<Permission>
            {
                new Permission { Name = "showMedication", Description = "Ver Medicamentos", Module = "Medicamentos" },
                new Permission { Name = "createMedication", Description = "Crear Medicamentos", Module = "Medicamentos" },
                new Permission { Name = "updateMedication", Description = "Editar Medicamentos", Module = "Medicamentos" },
                new Permission { Name = "deleteMedication", Description = "Eliminar Medicamentos", Module = "Medicamentos" },
            };
        }
        private List<Permission> Users()
        {
            return new List<Permission>
            {
                new Permission { Name = "showUser", Description = "Ver Usuarios", Module = "Usuarios" },
                new Permission { Name = "createUser", Description = "Crear Usuarios", Module = "Usuarios" },
                new Permission { Name = "updateUser", Description = "Editar Usuarios", Module = "Usuarios" },
                new Permission { Name = "deleteUser", Description = "Eliminar Usuarios", Module = "Usuarios" },
            };
        }
        private List<Permission> MedicalOrderPermissions()
        {
            return new List<Permission>
            {
                new Permission { Name = "showMedicalOrder", Description = "Ver Órdenes Médicas", Module = "Órdenes Médicas" },
                new Permission { Name = "createMedicalOrder", Description = "Crear Órdenes Médicas", Module = "Órdenes Médicas" },
                new Permission { Name = "updateMedicalOrder", Description = "Editar Órdenes Médicas", Module = "Órdenes Médicas" },
                new Permission { Name = "deleteMedicalOrder", Description = "Eliminar Órdenes Médicas", Module = "Órdenes Médicas" },
            };
        }

        private List<Permission> MedicalHistoryPermissions()
        {
            {
                return new List<Permission> {

                new Permission { Name = "showMedicalHistory", Description = " Ver Historiales Clínicos", Module = "Historia Clínica"},
                new Permission { Name = "createMedicalHistory", Description = "Crear Historiales Clínicos", Module = "Historia Clínica" },
                new Permission { Name = "updateMedicalHistory", Description = "Editar Historiales Clínicos", Module = "Historia Clínica" },
                new Permission { Name = "deleteMedicalHistory", Description = "Eliminar  Historiales Clínicos", Module = "Historia Clínica" },

                };
            }
        }


        private List<Permission> RolesPermissions()
        {
            {
                return new List<Permission> {

                new Permission { Name = "showRoles", Description = " Ver Roles", Module = "Roles"},
                new Permission { Name = "createRoles", Description = "Crear Roles", Module = "Roles" },
                new Permission { Name = "updateRoles", Description = "Editar Roles", Module = "Roles" },
                new Permission { Name = "deleteRoles", Description = "Eliminar  Roles", Module = "Roles" },

                };
            }



        }
    }
}

