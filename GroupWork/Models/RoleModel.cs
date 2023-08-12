using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class RoleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int IsActive { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int AddedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public int UpdatedById { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
