namespace Automobiliu_Nuoma_Web_Api.IRepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.Models;

    public interface ICarRepository
    {
        Task<IEnumerable<Automobilis>> GetAllAutomobiliaiAsync();
        Task<Automobilis> GetAutomobilisByIdAsync(int id);
        Task AddAutomobilisAsync(Automobilis automobilis);
        Task UpdateAutomobilisAsync(Automobilis automobilis);
        Task DeleteAutomobilisAsync(int id);
        Task<IEnumerable<Automobilis>> GetLaisviAutomobiliaiAsync(DateTime pradziosData, DateTime pabaigosData);
    }

}
