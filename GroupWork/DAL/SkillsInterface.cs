using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface SkillsInterface
    {
        Task AddAsync(SkillsModel skillsModel);
        Task<SkillsModel> GetByEmpId(int empId);
        Task<SkillsModel> GetByIdAsync(int Id);
        Task UpdateAsync(SkillsModel skillsModel);
    }
}
