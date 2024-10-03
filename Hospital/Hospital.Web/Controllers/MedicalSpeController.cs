using Hospital.Web.Data.Entities;
using Hospital.Web.Services;
using Hospital.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Hospital.Web.DTOs;

namespace Hospital.Web.Controllers
{
    public class MedicalSpeController: Controller
    {     
        private readonly IMedicalSpeServices _medicalspeService;

        public MedicalSpeController(IMedicalSpeServices medicalpsaService)
        {
            _medicalspeService = medicalpsaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<MedicalSpe>> response = await _medicalspeService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MedicalSpeDTO dto = await _medicalspeService.CreateDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalSpeDTO udto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(udto);
                }

                Response<MedicalSpe> response = await _medicalspeService.CreateAsync(udto);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                //TODO: MOstrar el mensaje  de error 
                return View(response);
            }
            catch (Exception ex)
            {
                return View(udto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<MedicalSpeDTO> response = await _medicalspeService.GetOneAsycn(id);
            if (response.IsSuccess)
            {

                return View(response.Result);
            }
            //TODO: Mensaja de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalSpeDTO section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TODO: mensaje de error
                    return View(section);
                }
                Response<MedicalSpeDTO> response = await _medicalspeService.EditAsync(section);

                if (response.IsSuccess)
                {
                    //TODO: mensaje de exito
                    return RedirectToAction(nameof(Index));
                }
                //TODO: MOstrar el mensaje  de error 
                return View(response);
            }
            catch (Exception ex)
            {
                return View(section);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   //Este metodo redirecciona confirma la eliminacion
            try
            {
                await _medicalspeService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

          }
        }
}
