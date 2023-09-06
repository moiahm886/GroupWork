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
        public DbSet<ExperienceModel> tbExperience { get; set; }
        public DbSet<SocialMediaModel> tbEmployeeSocialMediaLinks { get; set; }
        public DbSet<QualificationModel> tbQualification { get; set; }
        public DbSet<SkillsModel> tbSkills { get; set; }
        public DbSet<SalaryModel> Salary { get; set; }
        public DbSet<LookupModel> Lookup { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AUTH");
            modelBuilder.Entity<CompanyModel>().ToTable("Company", schema: "MNG");
            modelBuilder.Entity<BranchModel>().ToTable("CompanyBranch", schema: "MNG");
            modelBuilder.Entity<LookupModel>().ToTable("Lookup", schema: "MNG");
            modelBuilder.Entity<EmployeeModel>().ToTable("tbEmployees", schema: "HR");
            modelBuilder.Entity<ExperienceModel>().ToTable("tbExperience", schema: "HR");
            modelBuilder.Entity<SocialMediaModel>().ToTable("tbEmployeeSocialMediaLinks", schema: "HR");
            modelBuilder.Entity<SkillsModel>().ToTable("tbSkills", schema: "HR");
            modelBuilder.Entity<QualificationModel>().ToTable("tbQualification", schema: "HR");
            modelBuilder.Entity<SalaryModel>().ToTable("Salary", schema: "PAY");
            base.OnModelCreating(modelBuilder);
        }
    }
}
