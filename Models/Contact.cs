using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace portfolioApi.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^(?:\+?(\d{1,3})[-.\s]?(\d{1,4})[-.\s]?(\d{1,4})[-.\s]?(\d{3,4})(?:[-.\s]?(\d{1,9}))?)$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        [Column(TypeName = "nvarchar(1000)")]
        public string Message { get; set; }
    }
}
