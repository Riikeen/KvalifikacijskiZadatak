using BussinesLogic.DTO_s;
using BussinesLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KvalifikacijskiZadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KvaroviController : ControllerBase
    {
        private readonly IKvaroviService _kvaroviService;

        public KvaroviController(IKvaroviService strojeviService)
        {
            _kvaroviService = strojeviService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllKvarovi(int pagesize , int pagenumber)
        {
            var data = await _kvaroviService.GetAllKvarovi(pagesize,pagenumber);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKvaroviById(int id)
        {
            var result = await _kvaroviService.GetKvaroviById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddKvar([FromBody] UpdateCreateKvaroviDTO kvaroviDTO)
        {
            try
            {
                await _kvaroviService.AddKvar(kvaroviDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateKvarStatus(int id, bool isResolved)
        {
            try
            {
                await _kvaroviService.UpdateKvarStatus(id, isResolved);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKvar(int id, [FromBody] UpdateCreateKvaroviDTO kvaroviDTO)
        {
            try
            {
                await _kvaroviService.UpdateKvar(id, kvaroviDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKvar(int id)
        {
            try
            {
                await _kvaroviService.DeleteKvar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
