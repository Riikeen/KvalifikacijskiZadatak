using BussinesLogic.DTO_s;
using BussinesLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KvalifikacijskiZadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrojeviController : ControllerBase
    {
        private readonly IStrojeviService _strojeviService;

        public StrojeviController(IStrojeviService strojeviService)
        {
            _strojeviService = strojeviService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStrojevi()
        {
            var data = await _strojeviService.GetAllStrojevi();
            return Ok(data);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrojeviById(int id)
        {
            var result = await _strojeviService.GetStrojeviById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddStroj([FromBody] UpdateCreateStrojeviDTO strojeviDTO)
        {
            try
            {
                await _strojeviService.AddStroj(strojeviDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStroj(int id, [FromBody] UpdateCreateStrojeviDTO strojeviDTO)
        {
            try 
            {
                await _strojeviService.UpdateStrojevi(id, strojeviDTO);
                return Ok();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStroj(int id)
        {
            try
            {
                await _strojeviService.DeleteStroj(id);
                return NoContent();   
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
