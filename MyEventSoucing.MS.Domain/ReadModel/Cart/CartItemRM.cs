using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.MS.Domain.ReadModel.Cart;

public class CartItemRM : IReadEntity
{
    public Guid Id { get; }
    public Guid CartId { get; }
    public Guid ProductId { get; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }

    public CartItemRM(
        Guid id,
        Guid cartId,
        Guid productId,
        string productName,
        int quantity)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
    }
}