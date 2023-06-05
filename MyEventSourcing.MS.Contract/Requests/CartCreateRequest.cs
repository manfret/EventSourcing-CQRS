using MediatR;
using MyEventSourcing.MS.Contract.DTO;

namespace MyEventSourcing.MS.Contract.Requests;

public class CartCreateRequest : IRequest<Response<CartDTO>>
{
    public Guid CustomerId { get; private set; }

    public CartCreateRequest(Guid customerId)
    {
        CustomerId = customerId;
    }
}