#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace EFLecture.Models;

public class Post
{
    [Key]//Primary Key
    public int PostId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    [MaxLength(30, ErrorMessage = "can't be longer than characters.")]
    public string Title { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    public string Body { get; set; }

    [Display(Name = "Image URL")]
    public string ImgUrl { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /****************************************************
    Relationship properties below

    Foreign Keys: id of a different (foreign) model

    Navigation props:
        data type is related model
        MUST use .Include for the nav prop data to be inlcuded via SQL JOIN
    ****************************************************/

    public int UserId { get; set; } //this foreign key HAS TO MATCH PRIMARY KEY property name
    public User? Author { get; set; }
}