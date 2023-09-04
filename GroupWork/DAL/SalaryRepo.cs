using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class SalaryRepo : SalaryInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public SalaryRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task AddAsync(SalaryModel salaryModel)
        {
            await managementDataContextClass.Salary.AddAsync(salaryModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task DeleteAsync(SalaryModel salaryModel)
        {
             managementDataContextClass.Salary.Remove(salaryModel);
             await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<SalaryModel> GetByEmpId(int EmpId)
        {
            return await managementDataContextClass.Salary.FirstOrDefaultAsync(u => u.EmpId == EmpId);
        }

        public async Task<SalaryModel> GetByIdAsync(int Id)
        {
            return await managementDataContextClass.Salary.FindAsync(Id);
        }

        public async Task UpdateAsync(SalaryModel salaryModel)
        {

            managementDataContextClass.Salary.Update(salaryModel);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
