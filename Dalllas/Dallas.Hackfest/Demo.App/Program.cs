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
            Stopwatch sw = new Stopwatch();

            Enumerable.Range(1, 100)
                .ToList()
                .ForEach(productId =>
                {
                    sw.Start();
                    var product = productRepository.GetByIdAsync(productId).GetAwaiter().GetResult();
                    Console.WriteLine("Latency={0}", sw.Elapsed);
                    Console.WriteLine(product);
                    sw.Stop();
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
