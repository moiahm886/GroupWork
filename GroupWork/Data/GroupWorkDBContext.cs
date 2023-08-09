using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.Data
{
    public class GroupWorkDBContext : DbContext
    {
        public GroupWorkDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<RegistrationModelClass> Registration { get; set; }
        public DbSet<AdminAllowanceModelClass>AdminTable { get; set; }
    }
}
