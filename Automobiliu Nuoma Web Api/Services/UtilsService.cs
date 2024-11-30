using Automobiliu_Nuoma_Web_Api.IRepositories;
using Automobiliu_Nuoma_Web_Api.IServices;

namespace Automobiliu_Nuoma_Web_Api.Services
{
    public class UtilsService : IUtilsService
    {
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly IEmployeeService _employeeService;
        private readonly IRentalService _rentalService;

        public UtilsService(ICarService carService, IClientService clientService, IEmployeeService employeeService, IRentalService rentalService)
        {
            _carService = carService;
            _clientService = clientService;
            _employeeService = employeeService;
            _rentalService = rentalService;
        }

        public async Task CreateBackupAsync(string backupDirectory)
        {
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            } 
            await BackupTableAsync(backupDirectory, "Automobiliai", await _carService.GetAllAutomobiliaiAsync());
            await BackupTableAsync(backupDirectory, "Klientai", await _clientService.GetAllKlientaiAsync());
            await BackupTableAsync(backupDirectory, "Darbuotojai", await _employeeService.GetAllDarbuotojaiAsync());
            await BackupTableAsync(backupDirectory, "NuomosUzsakymai", await _rentalService.GetAllNuomosUzsakymaiAsync());
        }
        private async Task BackupTableAsync<T>(string backupDirectory, string tableName, IEnumerable<T> data)
        {
            var filePath = Path.Combine(backupDirectory, $"{tableName}.txt");
            using var writer = new StreamWriter(filePath); foreach (var row in data)
            {
                var values = string.Join(",", row.GetType().GetProperties().Select(p => p.GetValue(row, null)));
                await writer.WriteLineAsync(values);
            } 
        }
    }

}

