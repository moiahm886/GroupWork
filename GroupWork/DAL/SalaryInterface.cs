using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface SalaryInterface
    {
        Task AddAsync(SalaryModel salaryModel);
        Task<SalaryModel> GetByEmpId(int EmpId);
        Task<SalaryModel> GetByIdAsync(int Id);
        Task UpdateAsync(SalaryModel salaryModel);
        Task DeleteAsync(SalaryModel salaryModel);
    }
}
