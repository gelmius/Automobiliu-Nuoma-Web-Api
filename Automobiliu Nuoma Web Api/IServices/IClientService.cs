﻿namespace Automobiliu_Nuoma_Web_Api.IServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.Models;

    public interface IClientService
    {
        Task<IEnumerable<Klientas>> GetAllKlientaiAsync();
        Task<Klientas> GetKlientasByIdAsync(int id);
        Task AddKlientasAsync(Klientas klientas);
        Task UpdateKlientasAsync(Klientas klientas);
        Task DeleteKlientasAsync(int id);
    }

}
