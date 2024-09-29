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
            await _appoimentsService.GetOneAsync(); 
            return View(await _appoimentsService.GetOneAsync());
        }
    }
}
