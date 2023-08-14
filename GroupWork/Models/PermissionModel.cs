using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class PermissionModel
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int ScreenId { get; set; }

        public int MenuId { get; set; }

        public int CanRead { get; set; }

        public int CanUpdate { get; set; }

        public int CanDelete { get; set; }

        public int CanInsert { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int AddedId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public int UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
