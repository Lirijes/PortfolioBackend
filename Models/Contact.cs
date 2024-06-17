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
        [RegularExpression(@"^(?:(?:\+46\s*(?:[1-9][0-9]{1,2})\s*-?\s*(?:[0-9]{2,3})\s*-?\s*(?:[0-9]{2,4})|0\s*(?:[1-9][0-9]?[0-9]?)\s*-?\s*(?:[0-9]{2,3})\s*-?\s*(?:[0-9]{4,}))$)", ErrorMessage = "Invalid Swedish phone number")]
        public string Phone { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        [Column(TypeName = "nvarchar(1000)")]
        public string Message { get; set; }
    }
}
