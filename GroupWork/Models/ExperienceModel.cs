using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class ExperienceModel
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmpJobTitle { get; set; }
        public string EmpCompanyName { get; set; }
        public string EmpPostionId { get; set; }
        public int EmpCountryId { get; set; }
        public DateTime EmpWorkStartDate { get; set; }
        public DateTime EmpWorkEndDate { get; set; }
        public string EmpResponsibilities { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
