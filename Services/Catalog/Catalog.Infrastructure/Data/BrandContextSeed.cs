using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class BrandCotextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool existBrand = brandCollection.Find(b => true).Any();
        string path = Path.Combine("Data","SeedData","brands.json");
        if (!existBrand)
        {
            var brandData = File.ReadAllText(path);
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brands != null)
            {
                foreach (var brand in brands)
                {
                    brandCollection.InsertOneAsync(brand);
                }
            }
        }
    }

}
