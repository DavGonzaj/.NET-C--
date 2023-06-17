#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Please add a a Date of Birth")]
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Chef> CreatedDishes { get; set; } = new List<Chef>();

    // public class CheckChefAge : ValidationAttribute
    // {
    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //     {
    //         int age = CalculateAge((DateTime)value);
    //         if (age < 18)
    //         {
    //             return new ValidationResult("Chefs must be at least 18 years old!");
    //         }
    //         return ValidationResult.Success;
    //     }
    // }

    public static int CalculateAge(DateTime dob)
    {
        int age = 0;
        age = DateTime.Now.Year - dob.Year;
        if (DateTime.Now.DayOfYear < dob.DayOfYear)
        {
            age--;
        }
        return age;
    }
}


