using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Models
{
    public class PhoneNumberRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^(?:(?:\+46|0)\s*(?:[1-9][0-9]?[0-9]?)\s*-?\s*(?:[0-9]{2,3})\s*-?\s*(?:[0-9]{4,}))$", ErrorMessage = "Invalid Swedish phone number")]
        public string PhoneNumber { get; set; }
    }
}
