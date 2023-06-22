using System.ComponentModel.DataAnnotations;
namespace BeltExam.Models;

public class UserCouponUses
{
    [Key]
    public int UserCouponUsesId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    public int UserId { get; set; }
    public User? User { get; set; }
    public int CouponId { get; set; }
    public Coupon? Coupon { get; set; }
}