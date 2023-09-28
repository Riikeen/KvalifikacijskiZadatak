using Dapper;
using Data.Interfaces;
using Data.Models;
using Npgsql;

namespace Data.Repositories
{
    public class DbService : IDbService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task CreateEntity<T>(string command, object parameters)
        {
            await using NpgsqlConnection connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(command, parameters);
        }

        public async Task<List<T>> GetAllAsync<T>(string command, object parameters)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            List<T> result = (await connection.QueryAsync<T>(command,parameters)).ToList();
            return result;
        }

        public async Task<T> GetByIdAsync<T>(string command, object parameters)
        {
            await using NpgsqlConnection connection = _dbConnectionFactory.CreateConnection();
            T? result = (await connection.QueryFirstOrDefaultAsync<T>(command,parameters));

            if (result == null)
            {
                throw new Exception("Result is null");
            }
                return result;
        }

        public async Task DeleteEntity(string command, object parameter)
        {
            await using NpgsqlConnection connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(command,parameter);
        }

        public async Task UpdateEntity<T>(string command, object parameters)
        {
            await using NpgsqlConnection connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(command,parameters);
        }

        
    }
}
