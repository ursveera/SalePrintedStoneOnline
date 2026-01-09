using DAO.Models;

namespace API.Repos
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Product>> GetCatalogAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductColor>> GetProductColorsAsync(int productId);
    }
}
