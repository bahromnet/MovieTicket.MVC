using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.MVC.Entities;

[Table("order_item")]
public class OrderItem
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Amount { get; set; }
    public double Price { get; set; }

    public int MovieId { get; set; }
    [ForeignKey("MovieId")]
    public Movie Movie { get; set; }

    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}
