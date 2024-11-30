namespace Automobiliu_Nuoma_Web_Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.IServices;
    using Automobiliu_Nuoma_Web_Api.Models;

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Darbuotojas>> GetAllDarbuotojaiAsync()
        {
            return await _employeeRepository.GetAllDarbuotojaiAsync();
        }

        public async Task<Darbuotojas> GetDarbuotojasByIdAsync(int id)
        {
            return await _employeeRepository.GetDarbuotojasByIdAsync(id);
        }

        public async Task AddDarbuotojasAsync(Darbuotojas darbuotojas)
        {
            await _employeeRepository.AddDarbuotojasAsync(darbuotojas);
        }

        public async Task UpdateDarbuotojasAsync(Darbuotojas darbuotojas)
        {
            await _employeeRepository.UpdateDarbuotojasAsync(darbuotojas);
        }

        public async Task DeleteDarbuotojasAsync(int id)
        {
            await _employeeRepository.DeleteDarbuotojasAsync(id);
        }
    }

}
