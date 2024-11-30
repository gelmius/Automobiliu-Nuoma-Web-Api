using Automobiliu_Nuoma_Web_Api.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Automobiliu_Nuoma_Web_Api.Controllers
{
    public class UtilsController : Controller
    {
        private readonly IUtilsService _utilsService;

        public UtilsController(IUtilsService utilsService)
        {
            _utilsService = utilsService;
        }
        [HttpPost("CreateBackup")]
        public async Task<IActionResult> CreateBackup([FromQuery] string backupDirectory)
        {
            if (String.IsNullOrEmpty(backupDirectory))
            {
                backupDirectory = Directory.GetCurrentDirectory();
            }
            await _utilsService.CreateBackupAsync(backupDirectory);
            return Ok("Backup created successfully."); 
        }
    }
}
