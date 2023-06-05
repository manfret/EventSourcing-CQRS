using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.MS.Domain.ReadModel.Cart;

public class CartRM : IReadEntity
{
    public Guid Id { get; }
    public int TotalItems { get; set; }
    public Guid CustomerId { get; }
    public string CustomerName { get; } = null!;
    public IEnumerable<CartItemRM> Items { get; }

    private CartRM()
    {
        Items = new List<CartItemRM>();
    }

    public CartRM(
        Guid id,
        int totalItems,
        Guid customerId,
        string customerName) : this()
    {
        Id = id;
        TotalItems = totalItems;
        CustomerId = customerId;
        CustomerName = customerName;
    }
}