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

    public class RentalRepository : IRentalRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<RentalRepository> _logger;

        public RentalRepository(IConfiguration configuration, ILogger<RentalRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _logger.LogInformation("RentalRepository initialized with connection string");
        }

        public async Task<IEnumerable<NuomosUzsakymas>> GetAllNuomosUzsakymaiAsync()
        {
            _logger.LogDebug("Executing GetAllNuomosUzsakymaiAsync");
            using var connection = new SQLiteConnection(_connectionString);
            var nuomosUzsakymai = await connection.QueryAsync<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai");
            _logger.LogInformation("Successfully retrieved all nuomos uzsakymai");
            return nuomosUzsakymai;
        }

        public async Task<NuomosUzsakymas> GetNuomosUzsakymasByIdAsync(int id)
        {
            _logger.LogDebug("Executing GetNuomosUzsakymasByIdAsync with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var nuomosUzsakymas = await connection.QuerySingleOrDefaultAsync<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai WHERE Id = @Id", new { Id = id });
            if (nuomosUzsakymas == null)
            {
                _logger.LogWarning("Nuomos uzsakymas with ID {Id} not found", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved nuomos uzsakymas with ID {Id}", id);
            }
            return nuomosUzsakymas;
        }

        public async Task AddNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas)
        {
            _logger.LogDebug("Executing AddNuomosUzsakymasAsync for uzsakymas with ID {Id}", uzsakymas.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "INSERT INTO NuomosUzsakymai (KlientasId, DarbuotojasId, AutomobilisId, PradziosData, PabaigosData, Kaina) VALUES (@KlientasId, @DarbuotojasId, @AutomobilisId, @PradziosData, @PabaigosData, @Kaina)";
            await connection.ExecuteAsync(sql, uzsakymas);
            _logger.LogInformation("Successfully added nuomos uzsakymas with ID {Id}", uzsakymas.Id);
        }

        public async Task UpdateNuomosUzsakymasAsync(NuomosUzsakymas uzsakymas)
        {
            _logger.LogDebug("Executing UpdateNuomosUzsakymasAsync for uzsakymas with ID {Id}", uzsakymas.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "UPDATE NuomosUzsakymai SET KlientasId = @KlientasId, DarbuotojasId = @DarbuotojasId, AutomobilisId = @AutomobilisId, PradziosData = @PradziosData, PabaigosData = @PabaigosData, Kaina = @Kaina WHERE Id = @Id";
            await connection.ExecuteAsync(sql, uzsakymas);
            _logger.LogInformation("Successfully updated nuomos uzsakymas with ID {Id}", uzsakymas.Id);
        }

        public async Task DeleteNuomosUzsakymasAsync(int id)
        {
            _logger.LogDebug("Executing DeleteNuomosUzsakymasAsync for uzsakymas with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "DELETE FROM NuomosUzsakymai WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
            _logger.LogInformation("Successfully deleted nuomos uzsakymas with ID {Id}", id);
        }
    }
}
