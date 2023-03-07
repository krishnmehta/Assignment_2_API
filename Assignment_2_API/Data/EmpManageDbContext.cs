using Assignment_2_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2_API.Data
{
    public class EmpManageDbContext : DbContext
    {
        public EmpManageDbContext(DbContextOptions options ): base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
