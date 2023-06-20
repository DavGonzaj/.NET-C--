#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProdsAndCats.Models;

public class Association
{

    [Key]
    public int AssociationId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product NavProduct { get; set; }
    public Category NavCategory { get; set; }
}


