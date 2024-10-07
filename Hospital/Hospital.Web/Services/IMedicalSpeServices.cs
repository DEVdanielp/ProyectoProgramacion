using Hospital.Web.Data.Entities;
using Hospital.Web.Data;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Web.Core;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IMedicalSpeServices
    {
        public Task<Response<MedicalSpe>> CreateAsync(MedicalSpeDTO dto);
        public Task<Response<List<MedicalSpe>>> GetListAsync();
        public Task<Response<MedicalSpeDTO>> EditAsync(MedicalSpeDTO dto);
        public Task<Response<MedicalSpeDTO>> GetOneAsycn(int id);
        public Task<Response<MedicalSpe>> DeleteAsync(int Id);
    }

    public class MedicalSpeServices : IMedicalSpeServices
    {
        private readonly DataContext _context;

        private readonly IConvertHelper _convertHelper;

        public MedicalSpeServices(DataContext context, IConvertHelper convertHelper)
        {
            _context = context;
            _convertHelper = convertHelper;
        }

        public async Task<Response<MedicalSpe>> CreateAsync(MedicalSpeDTO dto)
        {
            try
            {
                MedicalSpe Spe = _convertHelper.ToMedicalSpe(dto);
                await _context.MedicalSpe.AddAsync(Spe);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalSpe>.MakeResponseSuccess(Spe, "Seccion creada con exito ");

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalSpe>.MakeResponseFail(ex);
            }
        }


        public async Task<Response<List<MedicalSpe>>> GetListAsync()
        {
            try
            {
                List<MedicalSpe> medics = await _context.MedicalSpe.Include(u => u.UserDoctor).ToListAsync();
                return ResponseHelper<List<MedicalSpe>>.MakeResponseSuccess(medics);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<MedicalSpe>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalSpeDTO>> EditAsync(MedicalSpeDTO dto)
        {
            try
            {

                MedicalSpe medic = await _context.MedicalSpe.FirstOrDefaultAsync(u => u.Id == dto.Id);

                medic.Name = dto.Name;
                medic.UserDoctor = await _context.Users.FirstOrDefaultAsync(a => a.Id == dto.UserDoctorId);




                _context.MedicalSpe.Update(medic);
                await _context.SaveChangesAsync();

                return ResponseHelper<MedicalSpeDTO>.MakeResponseSuccess(dto, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalSpeDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalSpeDTO>> GetOneAsycn(int id)
        {
            try
            {
                MedicalSpe? medic = await _context.MedicalSpe.Include(b => b.UserDoctor).FirstOrDefaultAsync(a => a.Id == id);
                if (medic == null)
                {
                    return ResponseHelper<MedicalSpeDTO>.MakeResponseFail("En la seccion con el id indicado no existe");
                }
                MedicalSpeDTO dto = new MedicalSpeDTO
                {
                    Name = medic.Name,
                    UserDoctor = await _context.Users.Select(a => new SelectListItem
                    {
                        Text = $"{a.FirstName} {a.LastName}",
                        Value = a.Id.ToString()
                    }).ToListAsync(),

                };
                return ResponseHelper<MedicalSpeDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<MedicalSpeDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<MedicalSpe>> DeleteAsync(int Id)
        {
            MedicalSpe? medic = await _context.MedicalSpe.FirstOrDefaultAsync(a => a.Id == Id);

            _context.MedicalSpe.Remove(medic);
            await _context.SaveChangesAsync();
            return ResponseHelper<MedicalSpe>.MakeResponseSuccess(medic, "sección actualizada con éxito");

        }

    }
}

