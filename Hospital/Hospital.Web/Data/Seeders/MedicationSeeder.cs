using Hospital.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Data.Seeders
{
    public class MedicationSeeder
    {
        private readonly DataContext _context;

        public MedicationSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Medication> Medications = new List<Medication>
            {
                new Medication { CommercialName = "Paracetamol", ScientificName = "Acetaminofén", Group = "Analgésico y antipirético", Description = "Alivia el dolor y la fiebre", Laboratory = "Laboratorio A" },
                new Medication { CommercialName = "Ibuprofeno", ScientificName = "Ibuprofenum", Group = "Antiinflamatorio", Description = "Reduce la inflamación y el dolor", Laboratory = "Laboratorio B" },
                new Medication { CommercialName = "Amoxicilina", ScientificName = "Amoxicillin", Group = "Antibiótico", Description = "Trata infecciones bacterianas", Laboratory = "Laboratorio C" },
                new Medication { CommercialName = "Diazepam", ScientificName = "Diazepam", Group = "Ansiolítico", Description = "Reduce la ansiedad y relaja los músculos", Laboratory = "Laboratorio D" },
                new Medication { CommercialName = "Omeprazol", ScientificName = "Omeprazolum", Group = "Antibiótico", Description = "Reduce la producción de ácido gástrico", Laboratory = "Laboratorio E" },
                new Medication { CommercialName = "Lactulosa", ScientificName = "Lactulose", Group = "Laxante", Description = "Alivia el estreñimiento", Laboratory = "Laboratorio F" },
                new Medication { CommercialName = "Senósidos", ScientificName = "Sennosides", Group = "Laxante", Description = "Estimula los movimientos intestinales", Laboratory = "Laboratorio G" },
                new Medication { CommercialName = "Lorazepam", ScientificName = "Lorazepam", Group = "Ansiolítico", Description = "Trata la ansiedad a corto plazo", Laboratory = "Laboratorio H" },
                new Medication { CommercialName = "Simvastatina", ScientificName = "Simvastatin", Group = "Reductor de colesterol", Description = "Disminuye los niveles de colesterol en la sangre", Laboratory = "Laboratorio I" },
                new Medication { CommercialName = "Salbutamol", ScientificName = "Salbutamol", Group = "Broncodilatador", Description = "Abre las vías respiratorias en casos de asma", Laboratory = "Laboratorio J" },

            };

            foreach (Medication medication in Medications)
            {
                bool exists = await _context.Medications.AnyAsync(s => s.CommercialName == medication.CommercialName && 
                                                                       s.ScientificName == medication.ScientificName && 
                                                                       s.Group == medication.Group && 
                                                                       s.Description == medication.Description &&
                                                                       s.Laboratory == medication.Laboratory);

                if (!exists)
                {
                    await _context.AddAsync(medication);
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}