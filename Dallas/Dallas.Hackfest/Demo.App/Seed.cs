using Demo.App.Models;
using Demo.App.Repositories;
using System.Linq;

namespace Demo.App
{
    public static class Seed
    {
        public static void CreateDatabase(IProductRepository productRepository)
        {
            Enumerable.Range(1, 100)
                .ToList()
                .ForEach(productId =>
                {
                    productRepository.AddNewAsync(new Product
                    {
                        Id = productId,
                        Name = $"This product has id { productId }"

                    }).GetAwaiter().GetResult();
                });
        }
    }
}
