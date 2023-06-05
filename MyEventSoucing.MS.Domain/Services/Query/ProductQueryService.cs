using MyEventSourcing.MS.Domain.ReadModel.Product;
using MyEventSourcing.MS.Domain.Repository;

namespace MyEventSourcing.MS.Domain.Services.Query;

public class ProductQueryService
{
    private readonly IProductRepository _productRepository;

    public ProductQueryService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductRM?> ReadAsync(Guid id)
    {
        return await _productRepository.ReadAsync(id);
    }
}