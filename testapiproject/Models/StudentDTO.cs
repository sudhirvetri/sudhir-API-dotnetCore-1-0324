using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using testapiproject.Validators;

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

        [DateCheck]
        public DateTime AdmissionDate { get; set; }

    }
}