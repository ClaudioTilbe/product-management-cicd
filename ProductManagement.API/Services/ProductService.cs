using ProductManagement.API.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.API.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync()
        {
            var products =
                await _repository.GetAllAsync();

            return products.Select(p => new ProductResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive
            });
        }


        public async Task<ProductResponseDTO?> GetByIdAsync(int id)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive
            };
        }


        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            if (dto.Price <= 0)
            {
                throw new ArgumentException(
                    "Price must be greater than zero.");
            }

            if (dto.Stock < 0)
            {
                throw new ArgumentException(
                    "Stock cannot be negative.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(product);

            await _repository.SaveChangesAsync();

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive
            };
        }


        public async Task<bool> UpdateAsync(int id, UpdateProductDTO dto)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.IsActive = dto.IsActive;

            await _repository.UpdateAsync(product);

            await _repository.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            await _repository.DeleteAsync(product);

            await _repository.SaveChangesAsync();

            return true;
        }





    }
}
