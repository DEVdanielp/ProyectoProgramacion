using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Helpers
{
    public interface ICombosHelpers
    {
        Task<IEnumerable<SelectListItem>> GetComboHospitalRolesAsync();
        public Task<IEnumerable<SelectListItem>> GetComboRols();
        public Task<IEnumerable<SelectListItem>> GetComboDoctor();
        public Task<IEnumerable<SelectListItem>> GetComboMedications();
        public Task<IEnumerable<SelectListItem>> GetComboAppoiments();
        public Task<IEnumerable<SelectListItem>> GetComboPatient();
        public Task<IEnumerable<SelectListItem>> GetComboPermissions();
    }


    public class CombosHelper : ICombosHelpers
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboHospitalRolesAsync()
        {
            List<SelectListItem> list = await _context.HospitalRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()     
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboRols()
        {
            List<SelectListItem> list = await _context.HospitalRoles.Select(r => new SelectListItem
            {
                Text = $"{r.Name}",
                Value = r.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Rol",
                Value = "0"

            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboDoctor()
        {
            List<SelectListItem> list = await _context.Users.Where(u => u.HospitalRole.Name == "Doctor").Select(u => new SelectListItem
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


        public async Task<IEnumerable<SelectListItem>> GetComboPatient()
        {
            List<SelectListItem> list = await _context.Users.Select(u => new SelectListItem
            {

                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Paciente",
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
                Text = $"Fecha:{a.Date}, Hora:{a.Time}, Número Cita: {a.Id}",
                Value = a.Id.ToString()
            }).ToListAsync();
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboPermissions()
        {
            List<SelectListItem> list = await _context.Permissions.Select(a => new SelectListItem
            {
                Text = $"{a.Name}",
                Value = a.Id.ToString()
            }).ToListAsync();
            return list;
        }
        
    }
}