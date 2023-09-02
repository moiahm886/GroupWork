using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class SkillsRepo : SkillsInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public SkillsRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task AddAsync(SkillsModel skillsModel)
        {
            await managementDataContextClass.tbSkills.AddAsync(skillsModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<SkillsModel> GetByEmpId(int empId)
        {
            return await managementDataContextClass.tbSkills.FirstOrDefaultAsync(u => u.EmpId == empId);
        }

        public async Task<SkillsModel> GetByIdAsync(int Id)
        {
            return await managementDataContextClass.tbSkills.FindAsync(Id);
        }

        public async Task UpdateAsync(SkillsModel skillsModel)
        {
            managementDataContextClass.tbSkills.Update(skillsModel);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
