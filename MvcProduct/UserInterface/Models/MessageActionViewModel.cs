using System.ComponentModel.DataAnnotations;
namespace MvcProduct.UserInterface.Models
{
    public class MessageActionViewModel

    {
        [Required]
        [StringLength(100, ErrorMessage = "Message must be more than 5 characters", MinimumLength = 5)]
        public string? MessageAction { get; set; }
    }
}
