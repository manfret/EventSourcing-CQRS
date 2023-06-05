namespace MyEventSourcing.MS.Domain.Entities.Cart;

public class CartItem
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; }
    public int Quantity { get; }

    public CartItem(
        Guid cartItemId,
        Guid productId, 
        int quantity)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        Quantity = quantity;
    }

    public override bool Equals(object? obj)
    {
        var castedObj = obj as CartItem;
        return castedObj != null && Equals(castedObj.ProductId, ProductId)
                                 && Equals(castedObj.Quantity, Quantity);
    }

    public override int GetHashCode()
    {
        var hashCode = 76325633;
        hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
        hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"{{ ProductId: \"{ProductId}\", Quantity: {Quantity} }}";
    }
}