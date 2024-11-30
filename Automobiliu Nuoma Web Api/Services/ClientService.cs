namespace Automobiliu_Nuoma_Web_Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;

    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Klientas>> GetAllKlientaiAsync()
        {
            return await _clientRepository.GetAllKlientaiAsync();
        }

        public async Task<Klientas> GetKlientasByIdAsync(int id)
        {
            return await _clientRepository.GetKlientasByIdAsync(id);
        }

        public async Task AddKlientasAsync(Klientas klientas)
        {
            await _clientRepository.AddKlientasAsync(klientas);
        }

        public async Task UpdateKlientasAsync(Klientas klientas)
        {
            await _clientRepository.UpdateKlientasAsync(klientas);
        }

        public async Task DeleteKlientasAsync(int id)
        {
            await _clientRepository.DeleteKlientasAsync(id);
        }
    }

}
