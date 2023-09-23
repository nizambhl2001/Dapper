using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper.API.Context
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _ConnectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _ConnectionString = _configuration.GetConnectionString("DapperConnetionString");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_ConnectionString);
    }
}
