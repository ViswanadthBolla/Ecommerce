using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetPRoductByNameQuery : IRequest<IList<ProductResponse>>
{
    public string Name { get; set; }
    public GetPRoductByNameQuery(string name)
    {
        Name = name;
    }
}