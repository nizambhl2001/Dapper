using Dapper.API.DTO;
using Dapper.API.Models;

namespace Dapper.API.Repository
{
    public interface IComapanyRepositorycs
    {
        Task<IEnumerable<Companies>> GetCompanies();
        Task<Companies> GetCompany(int id);
        Task<Companies> CreateCompany(CreateCompanyDto companyDto);
        Task UpdateCompany(int id,CreateCompanyDto companyDto);
        Task DeleteCompany(int id);
        Task<Companies> GetCompaniesByEmployeeId(int id);
    }
}
