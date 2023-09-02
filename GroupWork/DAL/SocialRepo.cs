using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GroupWork.DAL
{
    public class SocialRepo : SocialInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public SocialRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task AddAsync(SocialMediaModel socialMediaModel)
        {
            await managementDataContextClass.tbEmployeeSocialMediaLinks.AddAsync(socialMediaModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<SocialMediaModel> GetByEmpId(int empId)
        {
            return await managementDataContextClass.tbEmployeeSocialMediaLinks.FirstOrDefaultAsync(u => u.EmpId == empId);
        }

        public async Task<SocialMediaModel> GetByIdAsync(int Id)
        {
            return await managementDataContextClass.tbEmployeeSocialMediaLinks.FindAsync(Id);
        }

        public async Task UpdateAsync(SocialMediaModel socialMediaModel)
        {
            managementDataContextClass.tbEmployeeSocialMediaLinks.Update(socialMediaModel);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
