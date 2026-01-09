using DAO.DTO;

namespace API.Repos
{
    public interface IProductRepository
    {
        Task<ApiResponse<int>> CreateProductAsync(CreateProductRequest request);
        Task<ApiResponse<ProductResponse>> GetProductAsync(int productId);
        Task<ApiResponse<bool>> UpdateProductAsync(int productId, CreateProductRequest request);
        Task<ApiResponse<bool>> DeleteProductAsync(int productId);
    }
}
