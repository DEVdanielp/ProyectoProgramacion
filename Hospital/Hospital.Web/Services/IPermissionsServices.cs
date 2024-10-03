using Hospital.Web.Core;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Services
{
    public interface IPermissionsServices
    {
        public Task<Response<List<Permissions>>> GetListAsync();
        public Task<Response<Permissions>> GetOneAsync(int Id);
        public Task<Response<Permissions>> EditAsync(Permissions model);
        public Task<Response<Permissions>> CreateAsync(Permissions model);
        public Task<Response<Permissions>> DeleteAsync(int id);

    }

    public class PermissionsService : IPermissionsServices
    {
        private readonly DataContext _context;

        public PermissionsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Permissions>> CreateAsync(Permissions model)
        {
            try
            {
                Permissions Permissions = new Permissions
                {
                    Name = model.Name,
                    Description = model.Description
                };

                await _context.Permissions.AddAsync(Permissions);
                await _context.SaveChangesAsync();

                return ResponseHelper<Permissions>.MakeResponseSuccess(Permissions, "seccion creada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Permissions>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Permissions>>> GetListAsync()
        {
            try
            {
                List<Permissions> Permissions = await _context.Permissions.ToListAsync();
                return ResponseHelper<List<Permissions>>.MakeResponseSuccess(Permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Permissions>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Permissions>> GetOneAsync(int Id)
        {
            try
            {
                Permissions? Permissions = await _context.Permissions.FirstOrDefaultAsync(m => m.Id == Id);

                if (Permissions is null)
                {
                    return ResponseHelper<Permissions>.MakeResponseFail("El id indicado no existe");
                }


                return ResponseHelper<Permissions>.MakeResponseSuccess(Permissions);

            }
            catch (Exception ex)
            {
                return ResponseHelper<Permissions>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Permissions>> EditAsync(Permissions model)
        {
            try
            {
                _context.Permissions.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Permissions>.MakeResponseSuccess(model, "seccion actualizada con exito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<Permissions>.MakeResponseFail(ex);
            }


        }

        public async Task<Response<Permissions>> DeleteAsync(int id)
        {
            try
            {
                Permissions? permissions = await _context.Permissions.FirstOrDefaultAsync(a => a.Id == id);
                if (permissions is null)
                {
                    return ResponseHelper<Permissions>.MakeResponseFail("El id indicado no existe");
                }
                _context.Permissions.Remove(permissions);
                await _context.SaveChangesAsync();
                return ResponseHelper<Permissions>.MakeResponseSuccess(permissions);


            }
            catch (Exception ex)
            {
                return ResponseHelper<Permissions>.MakeResponseFail(ex);
            }
        }


    }
}
