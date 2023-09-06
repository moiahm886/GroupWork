using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class LookupModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LookupId { get; set; }

        [Required]
        [StringLength(50)]
        public string LookupName { get; set; }

        [Required]
        public int IsActive { get; set; }

        [Required]
        public int TypeCode { get; set; }

        [Required]
        public int AddedBy { get; set; }

        [Required]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }
}
