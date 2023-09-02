namespace GroupWork.Models
{
    public class QualificationModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmpDegreeName { get; set; }
        public int EmpStartYear { get; set; }
        public int EmpEndYear { get; set; }
        public string EmpInstituteName { get; set; }
        public int EmpInstituteCountryId { get; set; }
        public int EmpInstituteCityId { get; set; }
        public string EmpObtaineGPAPercentage { get; set; }
        public int EmpQuilficationTypeId { get; set; }
        public string EmpDescription { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
