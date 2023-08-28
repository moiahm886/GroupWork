using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface PermissionInterface
    {
        Task<List<RoleModel>> GetRolesAsync();
        PermissionModel permission(int Id);
        RoleModel role();
        Task AddRoles(RoleModel role);
        Task UpdatePermission(PermissionModel permission);
        Task<PermissionModel> FindPermsission(int Id);
    }
}
