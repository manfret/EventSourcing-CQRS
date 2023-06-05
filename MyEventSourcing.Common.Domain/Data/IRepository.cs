using System.Linq.Expressions;
using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.Common.Domain.Data;

public interface IRepository<T>
    where T : IReadEntity
{
    IQueryable<T> ReadAsync(Expression<Func<T, bool>> predicate);
    Task<T?> ReadAsync(Guid id);
    Task<T> CreateAsync(T entity);
    void Update(T entity);
}