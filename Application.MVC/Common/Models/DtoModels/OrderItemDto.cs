using Domain.MVC.Entities;

namespace Application.MVC.Common.Models.DtoModels;

public class OrderItemDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public Movie Movie { get; set; }
    public Order Order { get; set; }
}
