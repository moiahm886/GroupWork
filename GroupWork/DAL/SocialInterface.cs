using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface SocialInterface
    {
        Task AddAsync(SocialMediaModel socialMediaModel);
        Task<SocialMediaModel>GetByEmpId(int empId);
        Task<SocialMediaModel> GetByIdAsync(int Id);
        Task UpdateAsync(SocialMediaModel socialMediaModel);
    }
}
