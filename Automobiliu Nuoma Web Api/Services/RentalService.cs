namespace Automobiliu_Nuoma_Web_Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.Repositories;

    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly IReceiptRepository _receiptRepository;

        public RentalService(IRentalRepository rentalRepository, ICarRepository carRepository, IReceiptRepository receiptRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _receiptRepository = receiptRepository;
        }

        public async Task<IEnumerable<NuomosUzsakymas>> GetAllNuomosUzsakymaiAsync()
        {
            return await _rentalRepository.GetAllNuomosUzsakymaiAsync();
        }

        public async Task<NuomosUzsakymas> GetNuomosUzsakymasByIdAsync(int id)
        {
            return await _rentalRepository.GetNuomosUzsakymasByIdAsync(id);
        }

        public async Task AddNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas)
        {

            await _rentalRepository.AddNuomosUzsakymasAsync(uzsakymas);
            var automobilis = await _carRepository.GetAutomobilisByIdAsync(uzsakymas.AutomobilisId);
            await _receiptRepository.GenerateReceiptAsync(uzsakymas, automobilis);
        }
        

        public async Task UpdateNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas)
        {
            await _rentalRepository.UpdateNuomosUzsakymasAsync(uzsakymas);
        }

        public async Task DeleteNuomosUzsakymasAsync(int id)
        {
            await _rentalRepository.DeleteNuomosUzsakymasAsync(id);
        }
    }

}
