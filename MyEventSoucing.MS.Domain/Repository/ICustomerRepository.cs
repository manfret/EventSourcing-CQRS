using MyEventSourcing.MS.Domain.ReadModel.Customer;

namespace MyEventSourcing.MS.Domain.Repository;

public interface ICustomerRepository
{
    Task<CustomerRM?> ReadAsync(Guid id);
}