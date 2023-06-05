using MediatR;
using MyEventSourcing.MS.Contract.DTO;

namespace MyEventSourcing.MS.Contract.Requests;

public class CartReadRequest : IRequest<Response<CartDTO>>
{
    public Guid Id { get; }
    public bool IncludeItems { get; }

    public CartReadRequest(
        Guid id,
        bool includeItems)
    {
        Id = id;
        IncludeItems = includeItems;
    }
}