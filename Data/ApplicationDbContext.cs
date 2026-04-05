using Microsoft.EntityFrameworkCore;
using StudentManagementWeb.Models;

namespace StudentManagementWeb.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
