namespace Automobiliu_Nuoma_Web_Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;

    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Automobilis>> GetAllAutomobiliaiAsync()
        {
            _logger.LogDebug("Executing GetAllAutomobiliaiAsync");

            var automobiliai = await _carRepository.GetAllAutomobiliaiAsync();
            _logger.LogInformation("Successfully retrieved all automobiliai");

            return automobiliai;
        }

        public async Task<Automobilis> GetAutomobilisByIdAsync(int id)
        {
            _logger.LogDebug("Executing GetAutomobilisByIdAsync with id {Id}", id);
            var automobilis = await _carRepository.GetAutomobilisByIdAsync(id);
            if (automobilis == null)
            {
                _logger.LogError("Automobilis with id {Id} not found", id);
            }
            else
            {

                _logger.LogInformation("Successfully retrieved automobilis with id {Id}", id);
            }

            return automobilis;
        }

        public async Task AddAutomobilisAsync(Automobilis automobilis)
        {
            await _carRepository.AddAutomobilisAsync(automobilis);
        }

        public async Task UpdateAutomobilisAsync(Automobilis automobilis)
        {
            await _carRepository.UpdateAutomobilisAsync(automobilis);
        }

        public async Task DeleteAutomobilisAsync(int id)
        {
            await _carRepository.DeleteAutomobilisAsync(id);
        }
        public async Task<IEnumerable<Automobilis>> GetLaisviAutomobiliaiAsync(DateTime pradziosData, DateTime pabaigosData)
        {
            return await _carRepository.GetLaisviAutomobiliaiAsync(pradziosData, pabaigosData);
        }
    }
}

