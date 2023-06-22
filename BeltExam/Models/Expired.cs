#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExam.Models;
public class Expired
{
    [Key]
    public int ExpiredId { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
    public int CouponId { get; set; }
    public Coupon? Coupon { get; set; }
}
