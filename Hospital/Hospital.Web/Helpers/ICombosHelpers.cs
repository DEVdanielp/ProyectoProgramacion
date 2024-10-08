using Hospital.Web.Data;
using Hospital.Web.DTOs;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Helpers
{
    public interface ICombosHelpers
    {
        public Task<IEnumerable<SelectListItem>> GetComboRols();
        public Task<IEnumerable<SelectListItem>> GetComboUsers();
        public Task<IEnumerable<SelectListItem>> GetComboMedications();
        public Task<IEnumerable<SelectListItem>> GetComboAppoiments();
    }

    public class CombosHelper : ICombosHelpers
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboRols()
        {
            List<SelectListItem> list = await _context.Roles.Select(r => new SelectListItem
            {
                Text = $"{r.NameRol}",
                Value = r.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Rol",
                Value = "0"

            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboUsers()
        {
            List<SelectListItem> list = await _context.Users.Select(u => new SelectListItem
            {

                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Doctor",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboMedications()
        {
            List<SelectListItem> list = await _context.Medications.Select(a => new SelectListItem
            {
                Text = $"{a.CommercialName} {a.Description}",
                Value = a.Id.ToString()
            }).ToListAsync();
            return list;

        }
        public async Task<IEnumerable<SelectListItem>> GetComboAppoiments()
        {
            List<SelectListItem> list = await _context.Appoiments.Select(a => new SelectListItem
            {
                Text = $"Fecha:{a.Date}, Hora:{a.Time}, Doctor: {a.UserDoctorId}",
                Value = a.Id.ToString()
            }).ToListAsync();
            return list;
        }
    }
}
