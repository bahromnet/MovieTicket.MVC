using Application.MVC.Common.Services.Cart;

namespace Application.MVC.Common.Models.ViewModels;

public class ShoppingCartVM
{
    public ShoppingCart ShoppingCart { get; set; }
    public double ShoppingCartTotal { get; set; }
}
