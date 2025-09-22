using System.ComponentModel.DataAnnotations;

namespace MvcProduct.UserInterface.Models
{ 
    public enum FishBody //Water type drop downlist
    {
        Freshwater, 
        Saltwater
    }
    public class Fishs
    {
        public int Id { get; set; }
        public string? Fish { get; set; }
        [DataType(DataType.Date)]
        public DateTime CaughtDate { get; set; }

        [Display(Name = "Water Type")]
        public FishBody WaterType { get; set; }

        [Range(0, 100, ErrorMessage = "Must be between 0-100")]
        public int Age { get; set; }
        public decimal Price { get; set; }
    }
}
