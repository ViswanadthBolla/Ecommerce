using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class BrandCotextSeed
{
    public static async Task SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool existBrand = await brandCollection.Find(b => true).AnyAsync();
        if (!existBrand)
        {
            var brandData = await File.ReadAllTextAsync("../Catalog.Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brands != null && brands.Count > 0)
            {
                await brandCollection.InsertManyAsync(brands);
            }
        }
    }
}
