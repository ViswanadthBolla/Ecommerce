using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var ProductEntity = ProductMapper.Mapper.Map<Product>(request);
        if(ProductEntity == null)
        {
            throw new ApplicationException("Issue with mapping");
        }
        var newProduct = await _productRepository.CreateProductAsync(ProductEntity);
        var response = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        return response;
    }
}
