﻿namespace Automobiliu_Nuoma_Web_Api.IServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.Models;

    public interface IRentalService
    {
        Task<IEnumerable<NuomosUzsakymas>> GetAllNuomosUzsakymaiAsync();
        Task<NuomosUzsakymas> GetNuomosUzsakymasByIdAsync(int id);
        Task AddNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas);
        Task UpdateNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas);
        Task DeleteNuomosUzsakymasAsync(int id);
    }

}
