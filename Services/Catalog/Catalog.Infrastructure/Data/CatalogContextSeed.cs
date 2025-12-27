using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
     public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProducts = productCollection.Find(b => true).Any();
        string path = Path.Combine("Data","SeedData","products.json");
        if (!existProducts)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach (var product in products)
                {
                    productCollection.InsertOneAsync(product);
                }
            }
        }
    }

}   
