using ProductManagement.API.DTOs;

namespace ProductManagement.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDTO>> GetAllAsync();

        Task<ProductResponseDTO?> GetByIdAsync(int id);

        Task<ProductDTO> CreateAsync(CreateProductDTO dto);

        Task<bool> UpdateAsync(int id, UpdateProductDTO dto);

        Task<bool> DeleteAsync(int id);
    }


}
