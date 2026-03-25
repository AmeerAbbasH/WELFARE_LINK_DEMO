using System.ComponentModel.DataAnnotations;

namespace WELFARE_Application_MVCF.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
       // [Required]
        public string Name { get; set; }
        //[Required]
        public string Role { get; set; }
        //[Required]
        public string Email { get; set; }
        //[Required]
        public string Phone { get; set; }
        //[Required]
        public string Status { get; set; }
    }
}
