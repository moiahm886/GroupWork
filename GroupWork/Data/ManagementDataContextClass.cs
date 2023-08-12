using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.Data
{
    public class ManagementDataContextClass : DbContext
    {
        public ManagementDataContextClass(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> tbUsers { get; set; }
        public DbSet<RoleModel> tbRoles { get; set; }
        public DbSet<PermissionModel> tbPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AUTH"); 
            base.OnModelCreating(modelBuilder);
        }
    }
}
