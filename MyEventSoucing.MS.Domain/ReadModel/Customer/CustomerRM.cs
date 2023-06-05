using MyEventSourcing.Common.Domain.Data;

namespace MyEventSourcing.MS.Domain.ReadModel.Customer;

public class CustomerRM : IEntity
{
    public Guid Id { get; }
    public string Name { get; }

    public CustomerRM(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}