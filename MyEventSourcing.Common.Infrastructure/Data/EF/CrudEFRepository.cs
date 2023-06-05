using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyEventSourcing.Common.Domain.Data;

namespace MyEventSourcing.Common.Infrastructure.Data.EF;

public abstract class CrudEFRepository<TEntity, TContext>
    where TEntity : class, IEntity
    where TContext : BaseDbContext
{
    protected readonly TContext Context;
    protected readonly ILogger Logger;

    protected CrudEFRepository(TContext context, ILoggerFactory loggerFactory)
    {
        Context = context;
        Logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var res = await Context.Set<TEntity>().AddAsync(entity);
        return res.Entity;
    }

    public async Task CreateAsync(IEnumerable<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<TEntity?> ReadAsync(Guid id)
    {
        return await Context.Set<TEntity>().SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<TEntity>> ReadAsync(IEnumerable<Guid> ids)
    {
        return await Task.Run(() => Context.Set<TEntity>().Where(a => ids.Contains(a.Id)).ToList());
    }

    public IQueryable<TEntity> ReadAsync(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}