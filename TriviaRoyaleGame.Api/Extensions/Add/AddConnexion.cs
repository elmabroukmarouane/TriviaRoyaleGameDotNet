using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Npgsql;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TriviaRoyaleGame.Api.Extensions.Add;
public static class AddConnexion
{
    public static void AddConnection(this IServiceCollection self, IConfiguration configuration,
        IHostEnvironment env)
    {
        self.AddDbContext<DbContextTriviaRoyaleGame>(options =>
        {
            switch (configuration.GetSection("ConnectionType").Value)
            {
                case "SQLSERVER":
                    UseSqlServer(SqlServerConnectionStringBuilderFunction("SqlServerConnection", "SqlServerDbPassword", configuration), options, configuration, env);
                    break;
                case "POSTGRESQL":
                    UsePostgreSql(PostgreSqlConnectionStringBuilderFunction("PostgreSqlConnection", "PostgreSqlDbPassword", configuration), options, configuration, env);
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    break;
                case "SQLITE":
                    UseSqlite("SqliteConnection", options, configuration, env);
                    break;
                default:
                    UseSqlServer(SqlServerConnectionStringBuilderFunction("SqlServerConnection", "SqlServerDbPassword", configuration), options, configuration, env);
                    break;
            }
        });
        static string SqlServerConnectionStringBuilderFunction(string connectionStringName, string ConnectionConfigPart,
            IConfiguration configuration)
        {
            var builer = new SqlConnectionStringBuilder(
                    configuration.GetConnectionString(connectionStringName)
                );
            builer.Password = configuration.GetConnectionString(ConnectionConfigPart);
            return builer.ConnectionString;
        }

        static void UseSqlServer(string connectionString, DbContextOptionsBuilder options,
            IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                options
                    .UseSqlServer(
                            connectionString,
                            options => options.EnableRetryOnFailure()
                        ).EnableSensitiveDataLogging().EnableDetailedErrors();
            }
            else
            {
                options
                    .UseSqlServer(
                            connectionString,
                            options => options.EnableRetryOnFailure()
                        );
            }
        }

        static string PostgreSqlConnectionStringBuilderFunction(string connectionStringName, string ConnectionConfigPart,
            IConfiguration configuration)
        {
            var builer = new NpgsqlConnectionStringBuilder(
                    configuration.GetConnectionString(connectionStringName)
                );
            builer.Password = configuration.GetConnectionString(ConnectionConfigPart);
            return builer.ConnectionString;
        }

        static void UsePostgreSql(string connectionString, DbContextOptionsBuilder options,
            IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                options.UseNpgsql(
                            connectionString,
                            options => options.EnableRetryOnFailure()
                        ).EnableSensitiveDataLogging().EnableDetailedErrors();
            }
            else
            {
                options.UseNpgsql(
                    connectionString,
                    options => options.EnableRetryOnFailure()
                    );
            }
        }

        static void UseSqlite(string connectionString, DbContextOptionsBuilder options,
            IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                options.UseSqlite(
                    configuration.GetConnectionString(connectionString)
                   ).EnableSensitiveDataLogging().EnableDetailedErrors()
                   .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning)); // A Hack from dotnet community that must be removed when they fix the " The model for context 'DbContextTriviaRoyaleGame.Api' has pending changes. Add a new migration before updating the database. This exception can be suppressed or logged by passing event ID 'RelationalEventId.PendingModelChangesWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'. " Bug 
            }
            else
            {
                options.UseSqlite(
                    configuration.GetConnectionString(connectionString)
                    )
                    .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning)); // A Hack from dotnet community that must be removed when they fix the " The model for context 'DbContextTriviaRoyaleGame.Api' has pending changes. Add a new migration before updating the database. This exception can be suppressed or logged by passing event ID 'RelationalEventId.PendingModelChangesWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'. " Bug ;
            }
        }
    }
}
