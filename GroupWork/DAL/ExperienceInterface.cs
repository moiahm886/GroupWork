using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface ExperienceInterface
    {
        Task AddExperience(ExperienceModel experienceModel);
        Task<ExperienceModel> GetExperienceByEmpId(int Id);
        Task<ExperienceModel> GetExperienceById(int Id);
        Task UpdateAsync(ExperienceModel experienceModel);
    }
}
