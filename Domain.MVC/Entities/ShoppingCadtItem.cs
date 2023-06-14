using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MVC.Entities;

[Table("shopping_cart_item")]
public class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }

    public Movie Movie { get; set; }
    public int Amount { get; set; }


    public string ShoppingCartId { get; set; }
}
