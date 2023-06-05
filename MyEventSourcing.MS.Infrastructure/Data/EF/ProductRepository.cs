using Microsoft.Extensions.Logging;
using MyEventSourcing.Common.Infrastructure.Data.EF;
using MyEventSourcing.MS.Domain.ReadModel.Product;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Infrastructure.Data.EF;

public class ProductRepository : CrudEFRepository<ProductRM, MSDbContext>, IProductRepository
{
    public ProductRepository(MSDbContext context, ILoggerFactory loggerFactory) 
        : base(context, loggerFactory)
    {
    }
}