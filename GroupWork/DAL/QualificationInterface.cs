using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface QualificationInterface
    {
        Task AddAsync(QualificationModel qualificationModel);
        Task<QualificationModel> GetByEmpId(int empId);
        Task<QualificationModel> GetByIdAsync(int Id);
        Task UpdateAsync(QualificationModel qualificationModel);
    }
}
