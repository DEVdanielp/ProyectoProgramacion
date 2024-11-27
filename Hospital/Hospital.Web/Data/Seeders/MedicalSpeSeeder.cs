using Hospital.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Data.Seeders
{
    public class MedicalSpesSeeder
    {
        private readonly DataContext _context;

        public MedicalSpesSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<MedicalSpe> MedicalSpes = new List<MedicalSpe>
            {
               new MedicalSpe { Name = "Cardíologo", UserDoctorId = "" },
               new MedicalSpe { Name = "Ortopedista", UserDoctorId = ""},
               new MedicalSpe { Name = "Dermatología" , UserDoctorId = ""},
               new MedicalSpe { Name = "Ginecología", UserDoctorId ="" },
               new MedicalSpe { Name = "Pediatría", UserDoctorId = ""},
            };

            foreach (MedicalSpe MedicalSpe in MedicalSpes)
            {
                bool exists = await _context.MedicalSpe.AnyAsync(s => s.Name == MedicalSpe.Name);

                if (!exists)
                {
                    await _context.AddAsync(MedicalSpe);
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}