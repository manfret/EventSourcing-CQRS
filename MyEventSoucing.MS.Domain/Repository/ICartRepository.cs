using MyEventSourcing.Common.Domain.Data;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using System.Linq.Expressions;

namespace MyEventSourcing.MS.Domain.Repository;

public interface ICartRepository : IRepository<CartRM>
{
    Task<IEnumerable<CartRM>> ReadAsync(Expression<Func<CartRM, bool>> predicate, bool includeItems);
    Task<CartRM?> ReadAsync(Guid id, bool includeItems);
}