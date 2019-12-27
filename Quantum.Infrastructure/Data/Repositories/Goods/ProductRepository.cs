using Quantum.Core.Entities;
using Quantum.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Infrastructure.Data.Repositories
{
    public class ProductRepository : EfRepository<Product, string>, IProductRepository
    {
        private readonly QuantumDbContext _context;
        private readonly IAppLogger<EfRepository<Product, string>> _logger;
        private readonly IUserRepository _userRepository;

        public ProductRepository(QuantumDbContext dbContext, IAppLogger<EfRepository<Product, string>> logger, IUserRepository userRepository) : base(dbContext, logger)
        {
            _context = dbContext;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            var dbProduct = await GetByIdAsync(product.Id);
            return await DeleteAsync(dbProduct);
        }

        public async Task<int> GetCountAsync(ISpecification<Product> spec)
        {
            return await CountAsync(spec);
        }

        public async Task<Product> GetDetailsAsync(string Id)
        {
            var product = await GetByIdAsync(Id);
            product.CreatedBy = await _userRepository.GetUserById(product.CreatedById);
            product.ModifiedBy = product.ModifiedById != null ? await _userRepository.GetUserById(product.ModifiedById) : null;

            return product;
        }

        public async Task<List<Product>> ListAllProductsAsync()
        {
            var products = await ListAllAsync();

            foreach(var product in products)
            {
                product.CreatedBy = await _userRepository.GetUserById(product.CreatedById);
                product.ModifiedBy = product.ModifiedById != null ? await _userRepository.GetUserById(product.ModifiedById) : null;
            }

            //products.ForEach(async x => {
            //    x.CreatedBy = await _userRepository.GetUserById(x.CreatedById);
            //    x.ModifiedBy = x.ModifiedById != null ? await _userRepository.GetUserById(x.ModifiedById) : null;
            //});

            return products;
        }

        public async Task<List<Product>> ListProductsAsync(ISpecification<Product> spec)
        {
            var products = await ListAsync(spec);

            foreach (var product in products)
            {
                product.CreatedBy = await _userRepository.GetUserById(product.CreatedById);
                product.ModifiedBy = product.ModifiedById != null ? await _userRepository.GetUserById(product.ModifiedById) : null;
            }

            //products.ForEach(async x => {
            //    x.CreatedBy = await _userRepository.GetUserById(x.CreatedById);
            //    x.ModifiedBy = x.ModifiedById != null ? await _userRepository.GetUserById(x.ModifiedById) : null;
            //});

            return products;
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            var dbProduct = await GetByIdAsync(product.Id);
            dbProduct.Name = product.Name;
            dbProduct.HSNCode = product.HSNCode;
            dbProduct.Price = product.Price;
            dbProduct.Description = product.Description;
            dbProduct.ModifiedById = product.ModifiedById;

            return await UpdateAsync(dbProduct);
        }
    }
}
