using Microsoft.Data.Sqlite;
using System.Data;

/// <summary>
/// SQLite wrapper class
/// </summary>
public class DatabaseContext : IDisposable
{
    private IDbConnection _connection;

    /// <summary>
    /// Constructs a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    public DatabaseContext(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new SqliteConnection(connectionString);
    }

    /// <summary>
    /// Retrieves database connection
    /// </summary>
    public IDbConnection GetConnection()
    {
        return _connection;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}