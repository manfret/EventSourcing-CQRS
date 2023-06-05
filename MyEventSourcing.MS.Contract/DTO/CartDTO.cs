namespace MyEventSourcing.MS.Contract.DTO;

public class CartDTO
{
    public Guid Id { get; }
    public int TotalItems { get; }
    public Guid CustomerId { get; }
    public string CustomerName { get; }
    public IEnumerable<CartItemDTO> Items { get; }

    public CartDTO(
        Guid id,
        Guid customerId, 
        string customerName, 
        IEnumerable<CartItemDTO> items)
    {
        Id = id;
        TotalItems = items.Count();
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
    }
}