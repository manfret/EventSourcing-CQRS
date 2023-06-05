namespace MyEventSourcing.MS.Contract.DTO;

public class CartItemDTO
{
    public Guid Id { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }

    public CartItemDTO(
        Guid id,
        Guid productId, 
        string productName, 
        int quantity)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
    }
}