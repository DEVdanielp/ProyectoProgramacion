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
               new MedicalSpe { Name = "Cardíologo", UserDoctorId = "33333" },
               new MedicalSpe { Name = "Ortopedista", UserDoctorId = "44444"},
               new MedicalSpe { Name = "Dermatología" , UserDoctorId = "44444"},
               new MedicalSpe { Name = "Ginecología", UserDoctorId = "33333"},
               new MedicalSpe { Name = "Pediatría", UserDoctorId = "55555"},
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