using MyEventSourcing.MS.Domain.ReadModel.Product;

namespace MyEventSourcing.MS.Domain.Repository;

public interface IProductRepository
{
    Task<ProductRM?> ReadAsync(Guid id);
}