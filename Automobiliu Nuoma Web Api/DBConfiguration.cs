using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

public class DBConfiguration
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DBConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(_connectionString);
    }
}

