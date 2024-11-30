namespace Automobiliu_Nuoma_Web_Api.Controllers
{
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class NuomosUzsakymaiController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public NuomosUzsakymaiController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NuomosUzsakymas>>> GetAllNuomosUzsakymai()
        {
            var uzsakymai = await _rentalService.GetAllNuomosUzsakymaiAsync();
            return Ok(uzsakymai);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NuomosUzsakymas>> GetNuomosUzsakymasById(int id)
        {
            var uzsakymas = await _rentalService.GetNuomosUzsakymasByIdAsync(id);
            if (uzsakymas == null) return NotFound();
            return Ok(uzsakymas);
        }

        [HttpPost]
        public async Task<ActionResult> AddNuomosUzsakymas([FromBody] NuomosUzsakymas uzsakymas)
        {
            await _rentalService.AddNuomosUzsakymasAsync(uzsakymas);
            return CreatedAtAction(nameof(GetNuomosUzsakymasById), new { id = uzsakymas.Id }, uzsakymas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNuomosUzsakymas(int id, [FromBody] NuomosUzsakymas uzsakymas)
        {
            if (id != uzsakymas.Id) return BadRequest();
            await _rentalService.UpdateNuomosUzsakymasAsync(uzsakymas);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNuomosUzsakymas(int id)
        {
            await _rentalService.DeleteNuomosUzsakymasAsync(id);
            return NoContent();
        }
    }

}
