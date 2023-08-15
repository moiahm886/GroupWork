namespace GroupWork.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string? EmpCode { get; set; }
        public int? EmpDepartmetId { get; set; }
        public int? EmpSectionId { get; set; }
        public string? EmpFirstNameEn { get; set; }
        public string? EmpSecondNameEn { get; set; }
        public string? EmpLastNameEn { get; set; }
        public string? EmpFatherNameEn { get; set; }
        public string? EmpGrandNameEn { get; set; }
        public string? EmpFamilyNameEn { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpMobileNo { get; set; }
        public DateTime? EmpDOB { get; set; }
        public int? EmpBirthPlaceId { get; set; }
        public int? EmpNationalityId { get; set; }
        public string? EmpAge { get; set; }
        public string? EmpAddress { get; set; }
        public int? EmpGenderId { get; set; }
        public string? EmpReference { get; set; }
        public string? EmpReferenceContact { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? UpdatedBy{ get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? IsActive { get; set; }
        public string? ImageUrl { get; set; }
    }
}
