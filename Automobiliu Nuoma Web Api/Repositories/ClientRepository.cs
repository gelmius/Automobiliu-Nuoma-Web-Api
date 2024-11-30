namespace Automobiliu_Nuoma_Web_Api.Repositories
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Automobiliu_Nuoma_Web_Api.IRepositories;
    using Automobiliu_Nuoma_Web_Api.Models;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(IConfiguration configuration, ILogger<ClientRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _logger.LogInformation("ClientRepository initialized with connection string");
        }

        public async Task<IEnumerable<Klientas>> GetAllKlientaiAsync()
        {
            _logger.LogDebug("Executing GetAllKlientaiAsync");
            using var connection = new SQLiteConnection(_connectionString);
            var klientai = await connection.QueryAsync<Klientas>("SELECT * FROM Klientai");
            _logger.LogInformation("Successfully retrieved all klientai");
            return klientai;
        }

        public async Task<Klientas> GetKlientasByIdAsync(int id)
        {
            _logger.LogDebug("Executing GetKlientasByIdAsync with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var klientas = await connection.QuerySingleOrDefaultAsync<Klientas>("SELECT * FROM Klientai WHERE Id = @Id", new { Id = id });
            if (klientas == null)
            {
                _logger.LogWarning("Klientas with ID {Id} not found", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved klientas with ID {Id}", id);
            }
            return klientas;
        }

        public async Task AddKlientasAsync(Klientas klientas)
        {
            _logger.LogDebug("Executing AddKlientasAsync for klientas with ID {Id}", klientas.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "INSERT INTO Klientai (Vardas, Pavarde, ElPastas, Telefonas) VALUES (@Vardas, @Pavarde, @ElPastas, @Telefonas)";
            await connection.ExecuteAsync(sql, klientas);
            _logger.LogInformation("Successfully added klientas with ID {Id}", klientas.Id);
        }

        public async Task UpdateKlientasAsync(Klientas klientas)
        {
            _logger.LogDebug("Executing UpdateKlientasAsync for klientas with ID {Id}", klientas.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "UPDATE Klientai SET Vardas = @Vardas, Pavarde = @Pavarde, ElPastas = @ElPastas, Telefonas = @Telefonas WHERE Id = @Id";
            await connection.ExecuteAsync(sql, klientas);
            _logger.LogInformation("Successfully updated klientas with ID {Id}", klientas.Id);
        }

        public async Task DeleteKlientasAsync(int id)
        {
            _logger.LogDebug("Executing DeleteKlientasAsync for klientas with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "DELETE FROM Klientai WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
            _logger.LogInformation("Successfully deleted klientas with ID {Id}", id);
        }
    }
}
