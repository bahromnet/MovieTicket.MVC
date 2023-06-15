using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.MVC.Entities;

[Table("order")]
public class Order
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Email { get; set; }

    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public UserApplication User { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}
