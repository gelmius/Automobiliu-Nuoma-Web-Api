namespace Automobiliu_Nuoma_Web_Api.Repositories
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _logger.LogInformation("EmployeeRepository initialized with connection string");
        }

        public async Task<IEnumerable<Darbuotojas>> GetAllDarbuotojaiAsync()
        {
            _logger.LogDebug("Executing GetAllDarbuotojaiAsync");
            using var connection = new SQLiteConnection(_connectionString);
            var darbuotojai = await connection.QueryAsync<Darbuotojas>("SELECT * FROM Darbuotojai");
            _logger.LogInformation("Successfully retrieved all darbuotojai");
            return darbuotojai;
        }

        public async Task<Darbuotojas> GetDarbuotojasByIdAsync(int id)
        {
            _logger.LogDebug("Executing GetDarbuotojasByIdAsync with ID {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            var darbuotojas = await connection.QuerySingleOrDefaultAsync<Darbuotojas>("SELECT * FROM Darbuotojai WHERE Id = @Id", new { Id = id });
            if (darbuotojas == null)
            {
                _logger.LogWarning("Darbuotojas with ID {Id} not found", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved darbuotojas with ID {Id}", id);
            }
            return darbuotojas;
        }

        public async Task AddDarbuotojasAsync(Darbuotojas darbuotojas)
        {
            _logger.LogDebug("Executing AddDarbuotojasAsync for darbuotojas with ID {Id}", darbuotojas.Id);
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Darbuotojai (Vardas, Pavarde, Pareigos) VALUES (@Vardas, @Pavarde, @Pareigos)";
            await connection.ExecuteAsync(sql, darbuotojas);
            _logger.LogInformation("Successfully added darbuotojas with ID {Id}", darbuotojas.Id);
        }

        public async Task UpdateDarbuotojasAsync(Darbuotojas darbuotojas)
        {
            _logger.LogDebug("Executing UpdateDarbuotojasAsync for darbuotojas with ID {Id}", darbuotojas.Id);
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Darbuotojai SET Vardas = @Vardas, Pavarde = @Pavarde, Pareigos = @Pareigos WHERE Id = @Id";
            await connection.ExecuteAsync(sql, darbuotojas);
            _logger.LogInformation("Successfully updated darbuotojas with ID {Id}", darbuotojas.Id);
        }

        public async Task DeleteDarbuotojasAsync(int id)
        {
            _logger.LogDebug("Executing DeleteDarbuotojasAsync for darbuotojas with ID {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Darbuotojai WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
            _logger.LogInformation("Successfully deleted darbuotojas with ID {Id}", id);
        }
    }
}
