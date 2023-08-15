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
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<BranchModel> CompanyBranch { get; set; }
        public DbSet<EmployeeModel>tbEmployees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AUTH");
            modelBuilder.Entity<CompanyModel>().ToTable("Company", schema: "MNG");
            modelBuilder.Entity<BranchModel>().ToTable("CompanyBranch", schema: "MNG");
            modelBuilder.Entity<EmployeeModel>().ToTable("tbEmployees", schema: "HR");
            base.OnModelCreating(modelBuilder);
        }
    }
}
