using MyEventSourcing.MS.Domain.ReadModel.Cart;
using System.Linq.Expressions;

namespace MyEventSourcing.MS.Domain.Repository;

public interface ICartItemRepository
{
    Task<CartItemRM> CreateAsync(CartItemRM item);
    Task UpdateAsync(CartItemRM item);
    IQueryable<CartItemRM> ReadAsync(Expression<Func<CartItemRM, bool>> predicate);
}