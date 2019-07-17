using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}