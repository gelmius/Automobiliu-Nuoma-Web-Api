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

    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(IConfiguration configuration, ILogger<CarRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _logger.LogInformation("CarRepository initialized with connection string");
        }

        public async Task<IEnumerable<Automobilis>> GetAllAutomobiliaiAsync()
        {
            _logger.LogDebug("Executing GetAllAutomobiliaiAsync");
            using var connection = new SQLiteConnection(_connectionString);
            var automobiliai = await connection.QueryAsync<Automobilis>("SELECT * FROM Automobiliai");
            _logger.LogInformation("Successfully retrieved all automobiliai");
            return automobiliai;
        }

        public async Task<Automobilis> GetAutomobilisByIdAsync(int id)
        {
            _logger.LogDebug("Executing GetAutomobilisByIdAsync with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var automobilis = await connection.QuerySingleOrDefaultAsync<Automobilis>("SELECT * FROM Automobiliai WHERE Id = @Id", new { Id = id });
            if (automobilis == null)
            {
                _logger.LogWarning("Automobilis with ID {Id} not found", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved automobilis with ID {Id}", id);
            }
            return automobilis;
        }

        public async Task AddAutomobilisAsync(Automobilis automobilis)
        {
            _logger.LogDebug("Executing AddAutomobilisAsync for automobilis with ID {Id}", automobilis.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "INSERT INTO Automobiliai (Pavadinimas, Metai, NuomosKaina) VALUES (@Pavadinimas, @Metai, @NuomosKaina)";
            await connection.ExecuteAsync(sql, automobilis);
            _logger.LogInformation("Successfully added automobilis with ID {Id}", automobilis.Id);
        }

        public async Task UpdateAutomobilisAsync(Automobilis automobilis)
        {
            _logger.LogDebug("Executing UpdateAutomobilisAsync for automobilis with ID {Id}", automobilis.Id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "UPDATE Automobiliai SET Pavadinimas = @Pavadinimas, Metai = @Metai, NuomosKaina = @NuomosKaina WHERE Id = @Id";
            await connection.ExecuteAsync(sql, automobilis);
            _logger.LogInformation("Successfully updated automobilis with ID {Id}", automobilis.Id);
        }

        public async Task DeleteAutomobilisAsync(int id)
        {
            _logger.LogDebug("Executing DeleteAutomobilisAsync for automobilis with ID {Id}", id);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "DELETE FROM Automobiliai WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
            _logger.LogInformation("Successfully deleted automobilis with ID {Id}", id);
        }

        public async Task<IEnumerable<Automobilis>> GetLaisviAutomobiliaiAsync(DateTime pradziosData, DateTime pabaigosData)
        {
            _logger.LogDebug("Executing GetLaisviAutomobiliaiAsync from {PradziosData} to {PabaigosData}", pradziosData, pabaigosData);
            using var connection = new SQLiteConnection(_connectionString);
            var sql = @"
            SELECT * FROM Automobiliai a
            WHERE NOT EXISTS (
                SELECT 1 FROM NuomosUzsakymai nu
                WHERE nu.AutomobilisId = a.Id
                AND (
                    (nu.PradziosData <= @PabaigosData AND nu.PabaigosData >= @PradziosData)
                )
            )";
            var laisviAutomobiliai = await connection.QueryAsync<Automobilis>(sql, new { PradziosData = pradziosData, PabaigosData = pabaigosData });
            _logger.LogInformation("Successfully retrieved available automobiliai from {PradziosData} to {PabaigosData}", pradziosData, pabaigosData);
            return laisviAutomobiliai;
        }
    }


}
