using System.ComponentModel.DataAnnotations;
namespace Petstagram.Models;
public class Pet
{
    [Required(ErrorMessage = "is required")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Pet Type:")]
    public string Type { get; set; }

    [Range(0, 120)]
    public int Age { get; set; }

    public bool IsLazy { get; set; }
}