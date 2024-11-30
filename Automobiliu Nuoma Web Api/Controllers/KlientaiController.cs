namespace Automobiliu_Nuoma_Web_Api.Controllers
{
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class KlientaiController : ControllerBase
    {
        private readonly IClientService _clientService;

        public KlientaiController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klientas>>> GetAllKlientai()
        {
            var klientai = await _clientService.GetAllKlientaiAsync();
            return Ok(klientai);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Klientas>> GetKlientasById(int id)
        {
            var klientas = await _clientService.GetKlientasByIdAsync(id);
            if (klientas == null) return NotFound();
            return Ok(klientas);
        }

        [HttpPost]
        public async Task<ActionResult> AddKlientas([FromBody] Klientas klientas)
        {
            await _clientService.AddKlientasAsync(klientas);
            return CreatedAtAction(nameof(GetKlientasById), new { id = klientas.Id }, klientas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateKlientas(int id, [FromBody] Klientas klientas)
        {
            if (id != klientas.Id) return BadRequest();
            await _clientService.UpdateKlientasAsync(klientas);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteKlientas(int id)
        {
            await _clientService.DeleteKlientasAsync(id);
            return NoContent();
        }
    }

}
