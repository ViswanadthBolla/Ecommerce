using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
     public static async Task SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProducts = await productCollection.Find(b => true).AnyAsync();
        if (!existProducts)
        {
            var productsData = await File.ReadAllTextAsync("../Catalog.Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null && products.Count > 0)
            {
                await productCollection.InsertManyAsync(products);
            }
        }
    }

}   
