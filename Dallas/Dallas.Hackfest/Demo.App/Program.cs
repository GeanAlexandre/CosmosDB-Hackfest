using Demo.App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var productReposiry = Setup();
            //Seed.CreateDatabase(productReposiry);
            ReadAllDataFromDatbase(productReposiry);

            Console.ReadKey();

        }

        static void ReadAllDataFromDatbase(IProductRepository productRepository)
        {

            Enumerable.Range(1, 100)
                .ToList()
                .ForEach(productId =>
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var product = productRepository.GetByIdAsync(productId).GetAwaiter().GetResult();
                    sw.Stop();
                    Console.WriteLine($"Latency {sw.ElapsedMilliseconds} Milliseconds");
                    Console.WriteLine(product);
                });

        }

        static IProductRepository Setup()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<ISettings>(Settings.Configure(configuration));
            services.AddSingleton<IProductRepository, ProductRepository>();

            return services.BuildServiceProvider().GetRequiredService<IProductRepository>();
        }
    }
}
