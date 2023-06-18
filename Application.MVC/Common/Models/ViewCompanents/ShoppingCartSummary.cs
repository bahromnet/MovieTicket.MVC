using Application.MVC.Common.Services.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.Common.Models.ViewCompanents;

public class ShoppingCartSummary : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;
    public ShoppingCartSummary(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCartItems();

        return View(items.Count);
    }
}
