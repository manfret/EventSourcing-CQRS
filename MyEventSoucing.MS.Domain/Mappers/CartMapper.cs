using MyEventSourcing.MS.Contract.DTO;
using MyEventSourcing.MS.Domain.ReadModel.Cart;

namespace MyEventSourcing.MS.Domain.Mappers;

public class CartMapper
{
    public CartDTO Map(CartRM item)
    {
        return new CartDTO(
            item.Id,
            item.CustomerId,
            item.CustomerName,
            item.Items.Select(Map));
    }

    public CartItemDTO Map(CartItemRM item)
    {
        return new CartItemDTO(
            item.Id,
            item.ProductId,
            item.ProductName,
            item.Quantity);
    }
}