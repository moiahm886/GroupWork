using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface EmployeeInterface
    {
        Task AddAsync(EmployeeModel employee);
        Task<EmployeeModel> FindAsync(int Id);
        Task Remove(EmployeeModel employee);
        Task UpdateAsync(EmployeeModel employee);
        Task<bool> existingEmployee(int? EmpId);
        
        Task<List<UserModel>> ActiveUser();
        Task<List<EmployeeModel>> GetEmployees();
        Task<bool> checkuniqueniess(string EmpCode);
        Task<EmployeeModel> getEmployee(string Empcode);
    }
}
