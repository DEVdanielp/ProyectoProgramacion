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
            List<Permission> permissions = [.. MedicalSpe()];

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

        private List<Permission> MedicalSpe()
        {
            return new List<Permission>
            {
                new Permission { Name = "showMedicalSpecialization", Description = "Ver Especialidades Medicas", Module = "MedicalSpe" },
                new Permission { Name = "createMedicalSpecialization", Description = "Crear Especialidades Medicas", Module = "MedicalSpe" },
                new Permission { Name = "editMedicalSpecialization", Description = "Editar Especialidades Medicas", Module = "MedicalSpe" },
                new Permission { Name = "deleteMedicalSpecialization", Description = "Eliminar Especialidades Medicas", Module = "MedicalSpe" },
            };
        }
    }
}
