#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategories.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductDescription { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association> Associations { get; set; } = new List<Association>();
}
