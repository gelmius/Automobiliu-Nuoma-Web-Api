namespace Automobiliu_Nuoma_Web_Api.Controllers
{
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AutomobiliaiController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<AutomobiliaiController> _logger;

        public AutomobiliaiController(ICarService carService, ILogger<AutomobiliaiController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Automobilis>>> GetAllAutomobiliai()
        {
            _logger.LogDebug("Received GET request for all automobiliai");
            var automobiliai = await _carService.GetAllAutomobiliaiAsync();
            _logger.LogInformation("Successfully handled GET request for all automobiliai");
            return Ok(automobiliai);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Automobilis>> GetAutomobilisById(int id)
        {
            _logger.LogDebug("Received GET request for automobilis with id {Id}", id);
            var automobilis = await _carService.GetAutomobilisByIdAsync(id);
            if (automobilis == null) { _logger.LogWarning("Automobilis with id {Id} not found", id);
                return NotFound(); }
            _logger.LogInformation("Successfully handled GET request for automobilis with id {Id}", id);
            return Ok(automobilis);
        }

        [HttpPost]
        public async Task<ActionResult> AddAutomobilis([FromBody] Automobilis automobilis)
        {
            _logger.LogDebug("Received POST request to add automobilis with ID {Id}", automobilis.Id);
            await _carService.AddAutomobilisAsync(automobilis); 
            _logger.LogInformation("Successfully added automobilis with ID {Id}", automobilis.Id);
            return CreatedAtAction(nameof(GetAutomobilisById), new { id = automobilis.Id }, automobilis);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAutomobilis(int id, [FromBody] Automobilis automobilis)
        {
            _logger.LogDebug("Received PUT request to update automobilis with ID {Id}", id);
            if (id != automobilis.Id) 
            { 
                _logger.LogWarning("ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", id, automobilis.Id);
                return BadRequest();
            }
            await _carService.UpdateAutomobilisAsync(automobilis);
            _logger.LogInformation("Successfully updated automobilis with ID {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutomobilis(int id)
        {
            _logger.LogDebug("Received DELETE request for automobilis with ID {Id}", id);
            await _carService.DeleteAutomobilisAsync(id);
            _logger.LogInformation("Successfully deleted automobilis with ID {Id}", id);
            return NoContent();
        }

        [HttpGet("laisvi/{pradziosData}/{pabaigosData}")]
        public async Task<ActionResult<IEnumerable<Automobilis>>> GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData)
        {
            _logger.LogDebug("Received GET request for laisvi automobiliai from {PradziosData} to {PabaigosData}", pradziosData, pabaigosData);
            var laisviAutomobiliai = await _carService.GetLaisviAutomobiliaiAsync(pradziosData, pabaigosData);
            if (laisviAutomobiliai == null)
            { 
                _logger.LogWarning("No available automobiliai found for the specified dates from {PradziosData} to {PabaigosData}", pradziosData, pabaigosData);
                return NotFound();
            }
            _logger.LogInformation("Successfully retrieved available automobiliai from {PradziosData} to {PabaigosData}", pradziosData, pabaigosData);
            return Ok(laisviAutomobiliai);
        }
    }

}
