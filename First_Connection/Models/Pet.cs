#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace YourProjectName.Models;
public class Pet
{
    [Key]
    public int PetsId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool HasFur { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

