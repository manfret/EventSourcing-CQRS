using MyEventSourcing.Common.Domain.Core;

namespace MyEventSourcing.MS.Domain.ReadModel.Product;

public class ProductRM : IReadEntity
{
    public Guid Id { get; }
    public string Name { get; }

    public ProductRM(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}