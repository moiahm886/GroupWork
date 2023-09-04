using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace GroupWork.DAL
{
    public class EmployeeRepo : EmployeeInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public EmployeeRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }

        public async Task<List<UserModel>> ActiveUser()
        {
            return await managementDataContextClass.tbUsers
            .Where(user => user.IsActive == 1)
            .ToListAsync();
        }

        public async Task AddAsync(EmployeeModel employee)
        {
            await managementDataContextClass.tbEmployees.AddAsync(employee);
            await managementDataContextClass.SaveChangesAsync();
        }


        public async Task<bool> checkuniqueniess(string EmpCode)
        {
            return await managementDataContextClass.tbEmployees.AnyAsync(e => e.EmpCode == EmpCode);
        }

        public async Task<bool> existingEmployee(int? EmpId)
        {
            return await managementDataContextClass.tbEmployees
                        .AnyAsync(employee => employee.EmpCode == EmpId.ToString());
        }

        public async Task<EmployeeModel> FindAsync(int Id)
        {
            return await managementDataContextClass.tbEmployees.FindAsync(Id);
        }

        public async Task<EmployeeModel> getEmployee(string EmpCode)
        {
            return await managementDataContextClass.tbEmployees.FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await managementDataContextClass.tbEmployees.ToListAsync();
        }

        

        public async Task Remove(EmployeeModel employee)
        {
            managementDataContextClass.tbEmployees.Remove(employee);
            await managementDataContextClass.SaveChangesAsync();
        }


      

        public async Task UpdateAsync(EmployeeModel employee)
        {
            managementDataContextClass.tbEmployees.Update(employee);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
