using Dapper;
using Npgsql;
using System.Data;

namespace PennyPincher.Repositories
{
    public class DbService: IDbService
    {
        private readonly IDbConnection _connection;
        public DbService(IConfiguration configuration) 
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"))
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

        }
        public async Task<T> GetAsync<T>(string query, object parms)
        {
            T? result = (await _connection.QueryAsync<T>(query, parms).ConfigureAwait(false)).FirstOrDefault();

            return result;
        }

        public async Task<List<T>> GetAllAsync<T>(string query, object parms)
        {
            List<T> results = new List<T>();
            results = (await _connection.QueryAsync<T>(query, parms)).ToList();

            return results;
        }

        public async Task<int> ModifyData<T>(string query, object parms)
        {
            int result = await _connection.ExecuteAsync(query, parms).ConfigureAwait(false);

            return result;
        }

        public async Task<T> ModifyDataReturning<T>(string query, object parms)
        {
            T? result = (await _connection.QueryAsync<T>(query, parms).ConfigureAwait(false)).FirstOrDefault();
            return result;
        }
    }
}
