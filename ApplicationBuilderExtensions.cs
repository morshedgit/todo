using Dapper;
/// <summary>
/// Initializes the database
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Initializes the database
    /// </summary>
    public static IApplicationBuilder UseDatabaseInitializer(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DatabaseContext>();
        using var connection = dbContext.GetConnection();
        connection.Open();
        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Todo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                IsCompleted INTEGER
            )
        ";
        connection.Execute(createTableQuery);

        return app;
    }
}