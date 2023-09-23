using Dapper.API.DTO;
using Dapper.API.Models;
using Dapper.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IComapanyRepositorycs _repository;

        public CompaniesController(IComapanyRepositorycs repository) => _repository = repository;
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _repository.GetCompanies();
            return Ok(companies);
        }
        [HttpGet("{id}",Name ="CompanyById")]

        public async Task<IActionResult> GetCompany(int id)
        {
            var companies = await _repository.GetCompany(id);
            return Ok(companies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto companyDto)
        {
            var createcompany = await _repository.CreateCompany(companyDto);
            return CreatedAtRoute("CompanyById", new { id = createcompany.Id }, createcompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CreateCompanyDto companyDto)
        {
            var updatecompany = await _repository.GetCompany(id);
            await _repository.UpdateCompany(id, companyDto);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var updatecompany = await _repository.GetCompany(id);
            if(updatecompany == null) return NotFound();
             await _repository.DeleteCompany(id);
            return NotFound();
        }

        [HttpGet("ByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompaniForId(int id)
        {
           var company = await _repository.GetCompaniesByEmployeeId(id);
            if (company == null) return NotFound();
            return Ok(company);
        }


    }
}
