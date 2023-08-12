using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int? IsActive { get; set; } = 0;
        public int? EmpId { get; set; }
        public int? RoleId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int AddedBy { get; set; }  = 1;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
