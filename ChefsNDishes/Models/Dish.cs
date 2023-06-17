#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    public string Name { get; set; }
    // [Required]
    // [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    // public string Chef { get; set; }
    // [Required(ErrorMessage = "must be greater than 0")]
    public int Tastiness { get; set; }
    [Required(ErrorMessage = "must be greater than 0")]
    public int Calories { get; set; }
    [Required(ErrorMessage = "Please add a description")]

    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int ChefId { get; set; } //this foreign key HAS TO MATCH PRIMARY KEY property name
    public Chef? Author { get; set; }
}

