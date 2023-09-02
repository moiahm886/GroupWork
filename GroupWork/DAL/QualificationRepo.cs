using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class QualificationRepo : QualificationInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public QualificationRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }
        public async Task AddAsync(QualificationModel qualificationModel)
        {
            await managementDataContextClass.tbQualification.AddAsync(qualificationModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<QualificationModel> GetByEmpId(int empId)
        {
            return await managementDataContextClass.tbQualification.FirstOrDefaultAsync(u => u.EmpId == empId);
        }

        public async Task<QualificationModel> GetByIdAsync(int Id)
        {
            return await managementDataContextClass.tbQualification.FindAsync(Id);
        }

        public async Task UpdateAsync(QualificationModel qualificationModel)
        {
            managementDataContextClass.tbQualification.Update(qualificationModel);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
