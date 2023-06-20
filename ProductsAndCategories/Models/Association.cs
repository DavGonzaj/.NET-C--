#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategories.Models;

public class Association
{

    [Key]
    public int AssociationId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    // public DateTime CreatedAt { get; set; } = DateTime.Now;
    // public DateTime UpdatedAt { get; set; } = DateTime.Now;
    //     public Product NavProduct { get; set; }
    //     public Category NavCategory { get; set; }
}


