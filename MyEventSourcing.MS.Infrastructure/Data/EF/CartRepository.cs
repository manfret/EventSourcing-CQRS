using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEventSourcing.Common.Infrastructure.Data.EF;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Infrastructure.Data.EF;

public class CartRepository : CrudEFRepository<CartRM, MSDbContext>, ICartRepository
{
    public CartRepository(MSDbContext context, ILoggerFactory loggerFactory)
        : base(context, loggerFactory)
    {
    }

    public async Task<IEnumerable<CartRM>> ReadAsync(Expression<Func<CartRM, bool>> predicate, bool includeItems)
    {
        if (includeItems) return await ReadAsync(predicate).Include(a => a.Items).ToListAsync();
        return ReadAsync(predicate);
    }

    public async Task<CartRM?> ReadAsync(Guid id, bool includeItems)
    {
        if (includeItems) return await Context.CartRMs.Include(a => a.Items).SingleOrDefaultAsync(a => a.Id == id);
        return await ReadAsync(id);
    }

    public new async Task<CartRM> CreateAsync(CartRM entity)
    {
        var res = await base.CreateAsync(entity);
        await Context.SaveChangesAsync();
        return res;
    }

    public new void Update(CartRM entity)
    {
        base.Update(entity);
        Context.SaveChanges();
    }
}