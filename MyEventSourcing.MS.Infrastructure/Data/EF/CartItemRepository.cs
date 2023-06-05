using Microsoft.Extensions.Logging;
using MyEventSourcing.Common.Infrastructure.Data.EF;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Infrastructure.Data.EF;

public class CartItemRepository : CrudEFRepository<CartItemRM, MSDbContext>, ICartItemRepository
{
    public CartItemRepository(MSDbContext context, ILoggerFactory loggerFactory) 
        : base(context, loggerFactory)
    {
    }

    public new async Task<CartItemRM> CreateAsync(CartItemRM item)
    {
        var res = await base.CreateAsync(item);
        await Context.SaveChangesAsync();
        return res;
    }

    public async Task UpdateAsync(CartItemRM item)
    {
        Update(item);
        await Context.SaveChangesAsync();
    }
}