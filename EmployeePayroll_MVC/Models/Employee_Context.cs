using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll_MVC.Models
{
    public class Employee_Context:DbContext
    {
        public Employee_Context(DbContextOptions option) : base(option)
        {
        }
        public DbSet<EmployeeModel> Details { get; set; }
    }
}
