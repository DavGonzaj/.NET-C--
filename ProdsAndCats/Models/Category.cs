#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProdsAndCats.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Association> Products { get; set; }
}
