#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExam.Models;
public class Coupon
{
    [Key]
    public int CouponId { get; set; }

    [Required(ErrorMessage = "is required")]
    public string Code { get; set; }

    [Required(ErrorMessage = "is required")]
    public string Website { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(10, ErrorMessage = "must be at least 10 characters")]
    public string Description { get; set; }


    public DateTime Created_at { get; set; } = DateTime.Now;
    public DateTime Updated_at { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User? Creator { get; set; }
    public bool IsExpired { get; set; }
    public int ExpiredCount { get; set; }
    public List<Expired> Expired { get; set; } = new List<Expired>();
    public List<UserCouponUses> Uses { get; set; } = new List<UserCouponUses>();
}