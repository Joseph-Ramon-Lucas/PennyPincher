namespace PennyPincher.Repositories
{
    public interface IDbService
    {
        Task<T> GetAsync<T>(string query, object parms);
        Task<List<T>> GetAllAsync<T>(string query, object parms);
        Task<int> ModifyData<T>(string query, object parms);
    }
}
