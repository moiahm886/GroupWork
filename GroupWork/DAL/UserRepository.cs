using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 

namespace GroupWork.DAL
{
    public class UserRepository : UserInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;
        public UserRepository(ManagementDataContextClass _context)
        {
            managementDataContextClass = _context;
        }

        public async Task AddUser(UserModel user)
        {
            await managementDataContextClass.tbUsers.AddAsync(user);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task DeleteUser(UserModel userModel)
        {
            managementDataContextClass.tbUsers.Remove(userModel);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return await managementDataContextClass.tbUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await managementDataContextClass.tbUsers.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await managementDataContextClass.tbUsers.ToListAsync();
        }

        public async Task<bool> Uniqueness(int? EmpId)
        {
            return await managementDataContextClass.tbUsers.AnyAsync(u => u.EmpId == EmpId);
        }

        public async Task UpdateUser(UserModel user)
        {
            managementDataContextClass.tbUsers.Update(user);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
