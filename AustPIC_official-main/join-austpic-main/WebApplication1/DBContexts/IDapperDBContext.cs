using Microsoft.Data.SqlClient;

namespace ClubWebsite.DBContexts
{
    public interface IDapperDBContext
    {
        SqlConnection GetDBConnection();
        ResponseObjectType GetInfo<ResponseObjectType>(object obj, string sp);
        ResponseObjectType GetInfo<ResponseObjectType>(object obj, string sp, int commandTimeout);
        Task<ResponseObjectType> GetInfoAsync<ResponseObjectType>(object obj, string sp);
        IEnumerable<ResponseObjectType> GetInfoList<ResponseObjectType>(object obj, string sp);
        IEnumerable<ResponseObjectType> GetInfoList<ResponseObjectType>(object obj, string sp, int commandTimeout);
        Task<IEnumerable<ResponseObjectType>> GetInfoListAsync<ResponseObjectType>(object obj, string sp);
        Task<ResponseObjectType> GetInfoAsync<ResponseObjectType>(object obj, string sp, int commandTimeout = -1);
        void BasicOperation(object obj, string sp);
        void BasicOperation(object obj, string sp, int commandTimeout);
        Task BasicOperationAsync(object obj, string sp);
        dynamic BasicQueryOperation(object obj, string sp);
        dynamic BasicQueryOperation(object obj, string sp, int commandTimeout);
        Task<dynamic> BasicQueryOperationAsync(object obj, string sp);
        IEnumerable<dynamic> BasicQueryOperationList(object obj, string sp);
        IEnumerable<dynamic> BasicQueryOperationList(object obj, string sp, int commandTimeout);
    }
}
