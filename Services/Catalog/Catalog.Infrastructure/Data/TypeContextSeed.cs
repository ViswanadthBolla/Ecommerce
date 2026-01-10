using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    public static async Task SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool existTypes = await typeCollection.Find(b => true).AnyAsync();
        if (!existTypes)
        {
            var typesData = await File.ReadAllTextAsync("../Catalog.Infrastructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null && types.Count > 0)
            {
                await typeCollection.InsertManyAsync(types);
            }
        }
    }
}
