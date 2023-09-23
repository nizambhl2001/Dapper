using Dapper.API.Context;
using Dapper.API.DTO;
using Dapper.API.Models;
using System.Data;

namespace Dapper.API.Repository
{
    public class CompanyRepository:IComapanyRepositorycs
    {
        private readonly DapperDbContext _context;

        public CompanyRepository(DapperDbContext context) => _context = context;

        public async Task<IEnumerable<Companies>> GetCompanies()
        {
            var query = "SELECT Id,Name as CompanyName,Address,Country FROM  Companies";
            using(var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Companies>(query);
                return companies.ToList();
            }
        }

        public async Task<Companies> GetCompany(int id)
        {
            var query = "SELECT * FROM  Companies Where Id = @Id";
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Companies>(query, new {id});
                return company;
            }   
        }

        public async Task<Companies> CreateCompany(CreateCompanyDto companyDto)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)"+"select cast(scope_identity() as int)";
            var paramiters = new DynamicParameters();
            paramiters.Add("Name",companyDto.Name,DbType.String);
            paramiters.Add("Address",companyDto.Address,DbType.String);
            paramiters.Add("Country", companyDto.Country,DbType.String);

            using(var conneciton = _context.CreateConnection())
            {
                //await conneciton.ExecuteAsync(query, paramiters);
                var id = await conneciton.QuerySingleAsync<int>(query, paramiters);
                var CreateComapny = new Companies
                {
                    Id = id,
                    CompanyName = companyDto.Name,
                    Address = companyDto.Address,
                    Country = companyDto.Country,
                };
                return CreateComapny;
            }
        }

        public async Task UpdateCompany(int id, CreateCompanyDto companyDto)
        {
           var query = "Update Companies Set Name = @Name,Address = @Address,Country = @Country)";
           var paramiters = new DynamicParameters();
            paramiters.Add("Id", id, DbType.Int32);
            paramiters.Add("Name", companyDto.Name, DbType.String);
            paramiters.Add("Address", companyDto.Address, DbType.String);
            paramiters.Add("Country", companyDto.Country, DbType.String);

            using(var conneciton = _context.CreateConnection())
            {
                await conneciton.ExecuteAsync(query, paramiters);
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM  Companies Where Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.ExecuteAsync(query, new { id });
               
            }
        }

        public async Task<Companies> GetCompaniesByEmployeeId(int id)
        {
            var procedure = "CompaniesbyEmployeeId";
            var paramiter = new DynamicParameters();
            paramiter.Add("Id",id, DbType.Int32,ParameterDirection.Input);
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Companies>(procedure, paramiter,commandType: CommandType.StoredProcedure);
                return company;
            }
        }
    }
}
