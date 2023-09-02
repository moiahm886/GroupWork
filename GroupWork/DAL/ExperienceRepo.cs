using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class ExperienceRepo:ExperienceInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public ExperienceRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }

        public async Task AddExperience(ExperienceModel experienceModel)
        {
            await managementDataContextClass.tbExperience.AddAsync(experienceModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<ExperienceModel> GetExperienceByEmpId(int Id)
        {
            return await managementDataContextClass.tbExperience.FirstOrDefaultAsync(u => u.EmpId == Id);
        }

        public async Task<ExperienceModel> GetExperienceById(int Id)
        {
            return await managementDataContextClass.tbExperience.FindAsync(Id);
        }

        public async Task UpdateAsync(ExperienceModel experienceModel)
        {
            managementDataContextClass.tbExperience.Update(experienceModel);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
