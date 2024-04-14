using System.ComponentModel.DataAnnotations;

namespace testapiproject.Models
{
    public class StudentDTO
    {
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int Phone { get; set; }
    }
}