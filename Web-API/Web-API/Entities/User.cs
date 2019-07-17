using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public int RoleID { get; set; }
        
        public Role Role { get; set; }
    }
}