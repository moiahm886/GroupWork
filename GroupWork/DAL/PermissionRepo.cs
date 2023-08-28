using GroupWork.Data;
using GroupWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupWork.DAL
{
    public class PermissionRepo:PermissionInterface
    {
        private readonly ManagementDataContextClass managementDataContextClass;

        public PermissionRepo(ManagementDataContextClass managementDataContextClass)
        {
            this.managementDataContextClass = managementDataContextClass;
        }

        public async Task AddRoles(RoleModel role)
        {
            await managementDataContextClass.tbRoles.AddAsync(role);
            await managementDataContextClass.SaveChangesAsync();
        }

        public async Task<PermissionModel> FindPermsission(int Id)
        {
            return await managementDataContextClass.tbPermissions.FirstOrDefaultAsync(r => r.RoleId == Id);
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            return await managementDataContextClass.tbRoles.ToListAsync();
        }

        public PermissionModel permission(int Id)
        {
            return managementDataContextClass.tbPermissions.FirstOrDefault(u => u.RoleId == Id);
        }
        public RoleModel role()
        {
            return managementDataContextClass.tbRoles.FirstOrDefault(e => e.Name == "Employee");
        }

        public async Task UpdatePermission(PermissionModel permission)
        {
            managementDataContextClass.tbPermissions.Update(permission);
            await managementDataContextClass.SaveChangesAsync();
        }
    }
}
