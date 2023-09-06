using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class LookupRepo : LookupInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public LookupRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task AddAsync(LookupModel model)
        {
            await managementDataContextClass.Lookup.AddAsync(model);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task DeleteAsync(LookupModel model)
        {
            managementDataContextClass.Lookup.Remove(model);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<LookupModel> FindByCompanyIdAsync(int companyId)
        {
            return await managementDataContextClass.Lookup.FirstOrDefaultAsync(u => u.CompanyId == companyId);
        }
    }
}
