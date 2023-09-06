namespace GroupWork.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string Logo { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string BankName { get; set; }
        public string BankIban { get; set; }
        public int IsActive { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
