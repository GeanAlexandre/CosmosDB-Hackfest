using Demo.App.Models;
using System.Threading.Tasks;

namespace Demo.App.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task AddNewAsync(Product product);
    }
}
