using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetPRoductByNameQueryHandler : IRequestHandler<GetPRoductByNameQuery, IList<ProductResponse>>
{
    IProductRepository _productRepository;
    public GetPRoductByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetPRoductByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByNameAsync(request.Name);
        var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(products);
        return productResponseList;
    }
}
