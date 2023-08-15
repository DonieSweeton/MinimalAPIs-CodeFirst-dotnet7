using Microsoft.EntityFrameworkCore;
using MinimalCodeFirst.Models;

namespace MinimalCodeFirst.Data
{
    public class CodeContext : DbContext
    {
        public CodeContext(DbContextOptions<CodeContext> options) : base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; }
    }
}
