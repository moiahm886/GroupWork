using System;
using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class SalaryModel
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string SalaryMonthYear { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int BasicSalary { get; set; }
        public int NumberOfTotalHours { get; set; }
        public double HourlyRate { get; set; } = 0.0;
        public double RecurringAmount { get; set; } = 0.0;
        public double MonthlyBonus { get; set; } = 0.0;
        public int OtHours { get; set; }
        public double OtAmount { get; set; } = 0.0;
        public int AbsentDays { get; set; }
        public double AbsentDeduction { get; set; } = 0.0;
        public double LoanDeduction { get; set; } = 0.0;
        public double TotalDeduction { get; set; } = 0.0;
        public double TranspotationAllowance { get; set; } = 0.0;
        public double HousingAllowance { get; set; } = 0.0;
        public double MealAllowance { get; set; } = 0.0;
        public double MobileAllowance { get; set; } = 0.0;
        public double InternetAllowance { get; set; } = 0.0;
        public double OtherAllowance { get; set; } = 0.0;
        public double TotalAllowance { get; set; } = 0.0;
        public double ProvidentFundDeduction { get; set; } = 0.0;
        public double TaxDeduction { get; set; } = 0.0;
        public double TotalSalary { get; set; } = 0.0;
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
}
