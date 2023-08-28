using GroupWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupWork.DAL
{
    public interface UserInterface
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetByIdAsync(int id);
         Task<UserModel> GetUserByUsernameAsync(string username);
        Task<bool> Uniqueness(int? EmpId);
        Task AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(UserModel userModel);
    }
}
