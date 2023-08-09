using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GroupWork.Models
{
    public class RegistrationModelClass
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int RoleID { get; set; }
        public bool AdminApproval { get; set; }
        public string RoleName { get; set; }    
    }
}
