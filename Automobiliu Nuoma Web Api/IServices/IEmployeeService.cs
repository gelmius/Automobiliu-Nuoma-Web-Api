﻿namespace Automobiliu_Nuoma_Web_Api.IServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.Models;

    public interface IEmployeeService
    {
        Task<IEnumerable<Darbuotojas>> GetAllDarbuotojaiAsync();
        Task<Darbuotojas> GetDarbuotojasByIdAsync(int id);
        Task AddDarbuotojasAsync(Darbuotojas darbuotojas);
        Task UpdateDarbuotojasAsync(Darbuotojas darbuotojas);
        Task DeleteDarbuotojasAsync(int id);
    }

}
