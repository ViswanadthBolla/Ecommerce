using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool existTypes = typeCollection.Find(b => true).Any();
        string path = Path.Combine("Data","SeedData","types.json");
        if (!existTypes)
        {
            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null)
            {
                foreach (var type in types)
                {
                    typeCollection.InsertOneAsync(type);
                }
            }
        }
    }

}
