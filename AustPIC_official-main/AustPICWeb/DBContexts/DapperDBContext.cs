using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

using System.Reflection;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace AustPICWeb.DBContexts
{
    public class DapperDBContext : IDapperDBContext
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;

        #endregion

        #region Ctor

        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #endregion

        #region Methods

        public SqlConnection GetDBConnection()
        {
            return (SqlConnection)db;
        }

        public ResponseObjectType GetInfo<ResponseObjectType>(object obj, string sp)
        {
            var result = db.QueryFirstOrDefault<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public ResponseObjectType GetInfo<ResponseObjectType>(object obj, string sp, int commandTimeout)
        {
            var result = db.QueryFirstOrDefault<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            return result;
        }

        public async Task<ResponseObjectType> GetInfoAsync<ResponseObjectType>(object obj, string sp)
        {
            var result = await db.QueryFirstOrDefaultAsync<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<ResponseObjectType> GetInfoAsync<ResponseObjectType>(object obj, string sp, int commandTimeout = -1)
        {
            var result = await db.QueryFirstOrDefaultAsync<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            return result;
        }

        public IEnumerable<ResponseObjectType> GetInfoList<ResponseObjectType>(object obj, string sp)
        {
            var result = db.Query<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<ResponseObjectType> GetInfoList<ResponseObjectType>(object obj, string sp, int commandTimeout)
        {
            var result = db.Query<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            return result;
        }

        public async Task<IEnumerable<ResponseObjectType>> GetInfoListAsync<ResponseObjectType>(object obj, string sp)
        {
            var result = await db.QueryAsync<ResponseObjectType>(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public void BasicOperation(object obj, string sp)
        {
            db.Query(sp, obj, commandType: CommandType.StoredProcedure);
        }

        public void BasicOperation(object obj, string sp, int commandTimeout)
        {
            db.Query(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        }

        public async Task BasicOperationAsync(object obj, string sp)
        {
            await db.QueryAsync(sp, obj, commandType: CommandType.StoredProcedure);
        }

        public dynamic BasicQueryOperation(object obj, string sp)
        {
            var result = db.QueryFirstOrDefault(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public dynamic BasicQueryOperation(object obj, string sp, int commandTimeout)
        {
            var result = db.QueryFirstOrDefault(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            return result;
        }

        public async Task<dynamic> BasicQueryOperationAsync(object obj, string sp)
        {
            var result = await db.QueryFirstOrDefaultAsync(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<dynamic> BasicQueryOperationList(object obj, string sp)
        {
            var result = db.Query(sp, obj, commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<dynamic> BasicQueryOperationList(object obj, string sp, int commandTimeout)
        {
            var result = db.Query(sp, obj, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            return result;
        }

        #endregion
    }
}
