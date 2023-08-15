namespace GroupWork.Models
{
    public class BranchModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
