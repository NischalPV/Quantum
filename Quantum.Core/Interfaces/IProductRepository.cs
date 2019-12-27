using Quantum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Core.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Product, string>
    {
        Task<List<Product>> ListAllProductsAsync();
        Task<List<Product>> ListProductsAsync(ISpecification<Product> spec);
        Task<Product> GetDetailsAsync(string Id);
        Task<int> GetCountAsync(ISpecification<Product> spec);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
    }
}
