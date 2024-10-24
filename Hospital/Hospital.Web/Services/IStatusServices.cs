using Hospital.Web.Core;
using Hospital.Web.Core.Pagination;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IStatusServices
    {
        public Task<Response<PaginationResponse<Status>>> GetListAsync(PaginationRequest request);
        public Task<Response<Status>> CreateAsync(StatusDTO model);
        public Task<Response<StatusDTO>> GetOneAsync(int Id);
        public Task<Response<StatusDTO>> EditAsync(StatusDTO status);
        public Task<Response<Status>> DeleteAsync(int Id);

    }

    public class StatusServices : IStatusServices
    {
        private readonly DataContext _context;
        private readonly IConvertHelper _converter;
        private readonly ICombosHelpers _combosHelpers;

        public StatusServices(DataContext context, IConvertHelper converter, ICombosHelpers combosHelpers)
        {
            _context = context;
            _converter = converter;
            _combosHelpers = combosHelpers;
        }

        public async Task<Response<Status>> CreateAsync(StatusDTO dto)
        {
            try
            {
                Status b = _converter.ToStatus(dto);
                await _context.Status.AddAsync(b);
                await _context.SaveChangesAsync();
                return ResponseHelper<Status>.MakeResponseSuccess(b, "sección creada con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Status>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Status>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Status> query = _context.Status.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    if (int.TryParse(request.Filter, out int filterValue))
                    {
                        // Filtramos por Id numérico igual al valor del filtro
                        query = query.Where(s => s.Id == filterValue);
                    }
                    //query = query.Where(s => s.Id.Contains(request.Filter.ToLower)));
                }

                PagedList<Status> list = await PagedList<Status>.ToPagedListAsync(query, request);
                PaginationResponse<Status> result = new PaginationResponse<Status>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };
                return ResponseHelper<PaginationResponse<Status>>.MakeResponseSuccess(result, "Estado obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Status>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<StatusDTO>> GetOneAsync(int id)
        {
            try
            {
                Status? status = await _context.Status.FirstOrDefaultAsync(a => a.Id == id);

                if (status is null)
                {
                    return ResponseHelper<StatusDTO>.MakeResponseFail("El id indicado no existe");
                }

                StatusDTO dto = new StatusDTO
                {
                    StatusAppoiment = status.StatusAppoiment,

                    Appoiment = await _combosHelpers.GetComboAppoiments()

                };
                return ResponseHelper<StatusDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<StatusDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<StatusDTO>> EditAsync(StatusDTO status)
        {
            try
            {
                Console.WriteLine(status.AppoimentId);
                Status prueba = await _context.Status.FirstOrDefaultAsync(u => u.Id == status.Id);
                
                prueba.StatusAppoiment = status.StatusAppoiment;
                prueba.Appoiment = await _context.Appoiments.FirstOrDefaultAsync(u => u.Id == status.AppoimentId);

                _context.Status.Update(prueba);
                await _context.SaveChangesAsync();

                return ResponseHelper<StatusDTO>.MakeResponseSuccess(status, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<StatusDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Status>> DeleteAsync(int Id)
        {
            Status? status = await _context.Status.FirstOrDefaultAsync(a => a.Id == Id);

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();
            return ResponseHelper<Status>.MakeResponseSuccess(status, "sección actualizada con éxito");

        }
    }
}
