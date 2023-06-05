using MyEventSourcing.MS.Domain.ReadModel.Customer;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Domain.Services.Query;

public class CustomerQueryService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerQueryService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerRM?> ReadAsync(Guid id)
    {
        return await _customerRepository.ReadAsync(id);

        //если в быстрой БД нет - читаем из медленной, т.к. Customer - это тоже аггрегат
        //если не аггрегат - то он должен быть полем Cart
    }
}