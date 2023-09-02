namespace GroupWork.Models
{
    public class SkillsModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int EmpSkillTypeId { get; set; }
        public int ExperienceInYears { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
