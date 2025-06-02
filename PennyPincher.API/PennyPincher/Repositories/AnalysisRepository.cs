using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly string _connectionString;

        public AnalysisRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
        }

        public async Task<int?> CreateAnalysisAsync(AnalysisForCreationDto analysis)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                int insertedId = await conn.ExecuteScalarAsync<int>(sql, analysis);
                return insertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Analysis>> GetAllAnalysesAsync()
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var allAnalysis = await conn.QueryAsync<Analysis>(sql);
            }
            catch (Exception)
            {
                throw;
            }

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Analysis>> GetAllAnalysesByTypeAsync()
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = @"";
                var allAnalysesByType = await conn.QueryAsync<Analysis>(sql);

                return allAnalysesByType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Analysis> GetAnalysisByIdAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var foundAnalysis = await conn.QuerySingleAsync<Analysis>(sql);

                return foundAnalysis;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAnalysisAsync(Analysis analysis)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var rowsAffected = await conn.ExecuteAsync(sql, analysis);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAnalysisAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var rowsAffected = await conn.ExecuteAsync(sql, id);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Analysis>> GetAllAnalysesByTypeAsync(AnalysisTypes analysis)
        {
            throw new NotImplementedException();
        }
    }
}
