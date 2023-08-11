using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCrud.Model
{
    public class UpdateUserViewModel
    {
        [Required]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserName", TypeName = "nvarchar(500)")]
        public string UserName { get; set; }

        [Column("UserPassword", TypeName = "nvarchar(50)")]
        public string UserPassword { get; set; }

        [Column("IsActive", TypeName = "int")]
        public int? IsActive { get; set; }

        [Column("EmpId", TypeName = "int")]
        public int? EmpId { get; set; }

        [Column("RoleId", TypeName = "int")]
        public int? RoleId { get; set; }

        [Column("CompanyId", TypeName = "int")]
        public int? CompanyId { get; set; }

        [Column("BranchId", TypeName = "int")]
        public int? BranchId { get; set; }

        [Column("AddedBy", TypeName = "int")]
        public int? AddedBy { get; set; }

        [Column("AddedDate", TypeName = "datetime")]
        public DateTime? AddedDate { get; set; }

        [Column("UpdatedBy", TypeName = "int")]
        public int? UpdatedBy { get; set; }

        [Column("UpdatedDate", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
