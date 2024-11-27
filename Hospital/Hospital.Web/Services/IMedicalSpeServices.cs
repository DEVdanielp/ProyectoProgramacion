using Hospital.Web.Data.Entities;
using Hospital.Web.Data;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Hospital.Web.Core;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Core.Pagination;

namespace Hospital.Web.Services
{
    public interface IMedicalSpeServices
    {
        public Task<Response<MedicalSpe>> CreateAsync(MedicalSpeDTO dto);
        public Task<Response<PaginationResponse<MedicalSpe>>> GetListAsync(PaginationRequest request);
        public Task<Response<MedicalSpeDTO>> EditAsync(MedicalSpeDTO dto);
        public Task<Response<MedicalSpeDTO>> GetOneAsycn(int id);
        public Task<Response<MedicalSpe>> DeleteAsync(int Id);
    }

    public class MedicalSpeServices : IMedicalSpeServices
    {
        private readonly DataContext _context;

        private readonly IConverterHelper _convertHelper;
        private readonly ICombosHelpers _combo;
        public MedicalSpeServices(DataContext context, IConverterHelper convertHelper, ICombosHelpers combo)
        {
            _context = context;
            _convertHelper = convertHelper;
            _combo = combo;
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

        public async Task<Response<PaginationResponse<MedicalSpe>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<MedicalSpe> query = _context.MedicalSpe.AsQueryable().Include(s => s.UserDoctor.HospitalRole);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()) 
                                          || s.UserDoctor.FirstName.Contains(request.Filter.ToLower())
                                          || s.UserDoctor.LastName.ToLower().Contains(request.Filter.ToLower())
                                          || s.UserDoctor.FullName.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<MedicalSpe> list = await PagedList<MedicalSpe>.ToPagedListAsync(query, request);

                PaginationResponse<MedicalSpe> result = new PaginationResponse<MedicalSpe>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<MedicalSpe>>.MakeResponseSuccess(result, "Especialidad Medica obtenidas con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<MedicalSpe>>.MakeResponseFail(ex);
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
                    UserDoctor = await _combo.GetComboDoctor()

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

