using Microsoft.Extensions.Logging;
using MyEventSourcing.Common.Infrastructure.Data.EF;
using MyEventSourcing.MS.Domain.ReadModel.Customer;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Infrastructure.Data.EF;

public class CustomerRepository : CrudEFRepository<CustomerRM, MSDbContext>, ICustomerRepository
{
    public CustomerRepository(MSDbContext context, ILoggerFactory loggerFactory) 
        : base(context, loggerFactory)
    {
    }
}