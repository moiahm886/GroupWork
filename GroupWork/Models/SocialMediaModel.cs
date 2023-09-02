using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class SocialMediaModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpId { get; set; }

        [StringLength(50)]
        public string FacebookLink { get; set; }

        [StringLength(50)]
        public string TwitterLink { get; set; }

        [StringLength(50)]
        public string InstagramLink { get; set; }

        [StringLength(50)]
        public string LinkedinLink { get; set; }

        [StringLength(50)]
        public string WebsiteLink { get; set; }

        [StringLength(10)]
        public string OtherLink { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public int CompanyId { get; set; }

        public int BranchId { get; set; }
    }
}
