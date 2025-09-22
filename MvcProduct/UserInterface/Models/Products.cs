using System.ComponentModel.DataAnnotations;

namespace MvcProduct.UserInterface.Models;

public class Products
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? ItemType { get; set; }
    public decimal Price { get; set; }
}