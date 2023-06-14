#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFLecture.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage = "is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(8, ErrorMessage = "must be at least 8 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped]//don't add to DB
    public string PasswordConfirm { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}