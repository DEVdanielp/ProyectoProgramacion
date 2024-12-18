﻿using Hospital.Web.Core;
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
    public interface IAppoimentServices
    {
        public Task<Response<PaginationResponse<Appoiment>>> GetListAsync(PaginationRequest request);
        public Task<Response<Appoiment>> CreateAsync(AppoimentDTO model);
        public Task<Response<AppoimentDTO>> GetOneAsync(int Id);
        public Task<Response<AppoimentDTO>> EditAsync(AppoimentDTO appoiment);
        public Task<Response<Appoiment>> DeleteAsync(int Id);

    }

    public class AppoimentServices : IAppoimentServices
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converter;
        private readonly ICombosHelpers _combo;
        public AppoimentServices(DataContext context, IConverterHelper converter, ICombosHelpers combo)
        {
            _context = context;
            _converter = converter;
            _combo = combo;
        }

        public async Task<Response<Appoiment>> CreateAsync(AppoimentDTO dto)
        {
            try { 
            Appoiment b = _converter.ToAppoiment(dto);
            
            await _context.Appoiments.AddAsync(b);
            await _context.SaveChangesAsync();
            return ResponseHelper<Appoiment>.MakeResponseSuccess(b, "sección creada con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Appoiment>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Appoiment>>> GetListAsync(PaginationRequest request)
        {
            try
            {
               
                IQueryable<Appoiment> query = _context.Appoiments
                                              .Include(a => a.UserDoctor)  // Primer include
                                              .Include(a => a.UserPatient)  // Segundo include
                                              .AsQueryable();
                Console.WriteLine(query.ToString());    

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    if (int.TryParse(request.Filter, out int filterValue))
                    {
                        // Filtramos por Id numérico igual al valor del filtro
                        query = query.Where(s => s.Id == filterValue);
                    }
                    //query = query.Where(s => s.Id.Contains(request.Filter.ToLower)));
                }

                PagedList<Appoiment> list = await PagedList<Appoiment>.ToPagedListAsync(query, request);

                PaginationResponse<Appoiment> result = new PaginationResponse<Appoiment> 
                {
                   List = list, 
                   TotalCount = list.TotalCount,
                   RecordsPerPage = list.RecordsPerPage,
                   CurrentPage = list.CurrentPage,
                   TotalPages = list.TotalPages,
                   Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<Appoiment>>.MakeResponseSuccess(result, "Citas obtenidas con éxisto");

            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Appoiment>>.MakeResponseFail(ex);
            }
        }


        public async Task<Response<AppoimentDTO>> GetOneAsync(int id)
        {
            try
            {
                Appoiment? appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == id);

                if (appoiment is null)
                {
                    return ResponseHelper<AppoimentDTO>.MakeResponseFail("El id indicado no existe");
                }

                AppoimentDTO dto = new AppoimentDTO
                {
                    Date = appoiment.Date,
                    Time = appoiment.Time,
                   
                    UserDoctor = await _combo.GetComboDoctor(),

                    UserPatient = await _combo.GetComboPatient(),
                };
                return ResponseHelper <AppoimentDTO>.MakeResponseSuccess(dto);

            }
            catch (Exception ex)
            {
                return ResponseHelper<AppoimentDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<AppoimentDTO>> EditAsync(AppoimentDTO appoiment)
        {
            try
            {

                Appoiment prueba = await _context.Appoiments.FirstOrDefaultAsync(u => u.Id == appoiment.Id);

                    prueba.Date = appoiment.Date;
                    prueba.Time = appoiment.Time;
                    //prueba.UserDoctor = await _context.Users.FirstOrDefaultAsync(u => u.Id == appoiment.User);


                _context.Appoiments.Update(prueba);
                await _context.SaveChangesAsync();

                return ResponseHelper<AppoimentDTO>.MakeResponseSuccess(appoiment, "sección actualizada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<AppoimentDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Appoiment>> DeleteAsync(int Id)
        {
            Appoiment? appoiment = await _context.Appoiments.FirstOrDefaultAsync(a => a.Id == Id);

            _context.Appoiments.Remove(appoiment);
            await _context.SaveChangesAsync();
            return ResponseHelper<Appoiment>.MakeResponseSuccess(appoiment, "sección actualizada con éxito");
  
        }


    }
}
