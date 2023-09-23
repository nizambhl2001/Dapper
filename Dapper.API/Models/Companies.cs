namespace Dapper.API.Models
{
    public class Companies
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public List<Employee> employees { get; set; } = new List<Employee>();   
    }
}
