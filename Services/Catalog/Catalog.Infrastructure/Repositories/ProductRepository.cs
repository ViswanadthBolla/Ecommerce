using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    public ICatalogContext _context { get; }

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand)
    {
        return await _context
        .Products
        .Find(p => p.Brands.Name == brand)
        .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await _context
        .Products
        .Find(p => p.Name.ToLower().Contains(name.ToLower()))
        .ToListAsync();
    }
    public async Task<IEnumerable<Product>> GetProductsByTypeAsync(string type)
    {
        return await _context
        .Products
        .Find(p => p.Types.Name == type)
        .ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }
    public async Task<bool> UpdateProductAsync(Product product)
    {
       var updatedProduct = await _context
        .Products
        .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var deletedProduct = await _context
        .Products
        .DeleteOneAsync(p => p.Id == id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await _context
        .Brands
        .Find(_ => true)
        .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
    {
        return await _context
        .Types
        .Find(_ => true)
        .ToListAsync();
    }

}
