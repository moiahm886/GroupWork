using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GroupWork.DAL
{
    public class CompanyRepo : CompanyInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public CompanyRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }

        public async Task AddBranch(BranchModel branch)
        {
            await managementDataContextClass.CompanyBranch.AddAsync(branch);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task AddCompany(CompanyModel company)
        {
            await managementDataContextClass.Company.AddAsync(company);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<List<BranchModel>> FindBranches(int ID)
        {
            return await managementDataContextClass.CompanyBranch.Where(b => b.CompanyId == ID).ToListAsync();
        }

        public async Task<BranchModel> FindBranchesById(int ID)
        {
            return await managementDataContextClass.CompanyBranch.FindAsync(ID);
        }

        public async Task<CompanyModel> FindBranchThroughCompany(int ID)
        {
            return await managementDataContextClass.Company.FirstOrDefaultAsync(r => r.Id == ID);
        }

        public async Task<CompanyModel> FindCompanyByID(int? ID)
        {
            return await managementDataContextClass.Company.FindAsync(ID);
        }

        public async Task<List<BranchModel>> GetBranches()
        {
            return await managementDataContextClass.CompanyBranch.ToListAsync();
        }

        public async Task<List<CompanyModel>> GetCompany()
        {
            return await managementDataContextClass.Company.ToListAsync();
        }

        public async Task RemoveBranch(BranchModel branch)
        {
            managementDataContextClass.CompanyBranch.Remove(branch);
            await managementDataContextClass.SaveChangesAsync();
        }

        public void RemoveBranches(List<BranchModel> branches)
        {
             managementDataContextClass.CompanyBranch.RemoveRange(branches);
        }

        public void RemoveCompany(CompanyModel company)
        {
            managementDataContextClass.Company.Remove(company);
        }

        public async Task savechanges()
        {
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
