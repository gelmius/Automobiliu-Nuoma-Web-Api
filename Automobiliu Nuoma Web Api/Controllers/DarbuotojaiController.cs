namespace Automobiliu_Nuoma_Web_Api.Controllers
{
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class DarbuotojaiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<DarbuotojaiController> _logger;

        public DarbuotojaiController(IEmployeeService employeeService, ILogger<DarbuotojaiController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Darbuotojas>>> GetAllDarbuotojai()
        {
            _logger.LogDebug("Received GET request for all darbuotojai");
            var darbuotojai = await _employeeService.GetAllDarbuotojaiAsync();
            _logger.LogInformation("Successfully handled GET request for all darbuotojai");
            return Ok(darbuotojai);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Darbuotojas>> GetDarbuotojasById(int id)
        {
            _logger.LogDebug("Received GET request for darbuotojas with ID {Id}", id);
            var darbuotojas = await _employeeService.GetDarbuotojasByIdAsync(id);
            if (darbuotojas == null)
            {
                _logger.LogWarning("Darbuotojas with ID {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Successfully handled GET request for darbuotojas with ID {Id}", id);
            return Ok(darbuotojas);
        }

        [HttpPost]
        public async Task<ActionResult> AddDarbuotojas([FromBody] Darbuotojas darbuotojas)
        {
            _logger.LogDebug("Received POST request to add darbuotojas with ID {Id}", darbuotojas.Id);
            await _employeeService.AddDarbuotojasAsync(darbuotojas);
            _logger.LogInformation("Successfully added darbuotojas with ID {Id}", darbuotojas.Id);
            return CreatedAtAction(nameof(GetDarbuotojasById), new { id = darbuotojas.Id }, darbuotojas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDarbuotojas(int id, [FromBody] Darbuotojas darbuotojas)
        {
            _logger.LogDebug("Received PUT request to update darbuotojas with ID {Id}", id);
            if (id != darbuotojas.Id)
            {
                _logger.LogWarning("ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", id, darbuotojas.Id);
                return BadRequest();
            }
            await _employeeService.UpdateDarbuotojasAsync(darbuotojas);
            _logger.LogInformation("Successfully updated darbuotojas with ID {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDarbuotojas(int id)
        {
            _logger.LogDebug("Received DELETE request for darbuotojas with ID {Id}", id);
            await _employeeService.DeleteDarbuotojasAsync(id);
            _logger.LogInformation("Successfully deleted darbuotojas with ID {Id}", id);
            return NoContent();
        }
    }


}
