using Microsoft.EntityFrameworkCore;

namespace MyEventSourcing.Common.Infrastructure.Data.EF;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) 
        : base(options)
    { }
}