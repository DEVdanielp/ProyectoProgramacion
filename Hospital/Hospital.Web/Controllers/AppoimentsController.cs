using Hospital.Web.Core;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Controllers
{
    public class AppoimentsController : Controller
    {
        readonly IAppoimentServices _appoimentsService;

        public AppoimentsController(IAppoimentServices appoimentsService)
        {
            _appoimentsService = appoimentsService;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<Appoiment>> response = await _appoimentsService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AppoimentDTO dto = await _appoimentsService.CreateDTO();
            return View(dto);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppoimentDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                Response<Appoiment> response = await _appoimentsService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {

            Response<AppoimentDTO> response = await _appoimentsService.GetOneAsync(Id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
          
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppoimentDTO appoiment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(appoiment);
                }

                Response<AppoimentDTO> response = await _appoimentsService.EditAsync(appoiment);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(response);
            }
            catch (Exception ex)
            {
                return View(appoiment);
            }
        }
    }
}
