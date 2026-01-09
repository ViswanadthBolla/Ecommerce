using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProdcutsByIdQuery : IRequest<ProductResponse>
{
    public string Id { get; set; }
    public GetProdcutsByIdQuery(string id)
    {
        Id = id;  
    }
}
