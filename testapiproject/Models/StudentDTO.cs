using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace testapiproject.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(30)]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string? Email { get; set; }

        [RegularExpression(@"^(\+91)?[6-9]\d{9}$", ErrorMessage = "Please enter a valid Indian phone number.")]
        public long Phone { get; set; }
        [Range(10, 20)]
        public int age { get; set; }
        public int password { get; set; }

        [Compare("password")]
        public int confirmpassword { get; set; }
    }
}