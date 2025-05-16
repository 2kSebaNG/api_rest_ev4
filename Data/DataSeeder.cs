
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ApiRest_Ev4.Data
{
    public class DataSeeder
    {
        public async static Task InitializeData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DataSeeder>>();

                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                try
                {
                    await context.Database.MigrateAsync();
                    if (!await context.Products.AnyAsync())
                    {
                        var products = new Faker<Product>()
                            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                            .RuleFor(p => p.Sku, f => f.Commerce.Ean8())
                            .RuleFor(p => p.Price, f => f.Random.Int(1,10000))
                            .RuleFor(p => p.Stock, f => f.Random.Int(1, 100))
                            .RuleFor(p => p.IsActive, f => f.Random.Bool(0.8f))
                            .Generate(150);

                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ha ocurrido un error al cargar los datos semillas");
                }
            }
        }
    }
}