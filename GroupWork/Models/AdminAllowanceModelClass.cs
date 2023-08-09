using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupWork.Models
{
    public class AdminAllowanceModelClass
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(ID))]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int IsActive { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
