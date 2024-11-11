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
            List<Permission> permissions = [.. MedicalSpes(),..Medications(), ..Users()];

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


    }
}
