using DAO.Models;
using SqlKata.Execution;

namespace API.Repos
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly QueryFactory _db;

        public CatalogRepository(QueryFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetCatalogAsync()
        {
            return await _db.Query("Products")
                .Where("IsActive", true)
                .Select(
                    "ProductId",
                    "Name",
                    "Description",
                    "BasePrice",
                    "DefaultImageUrl"
                )
                .GetAsync<Product>();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _db.Query("Products")
                .Where("ProductId", productId)
                .FirstOrDefaultAsync<Product>();
        }

        public async Task<IEnumerable<ProductColor>> GetProductColorsAsync(int productId)
        {
            return await _db.Query("ProductColors")
                .Where("ProductId", productId)
                .GetAsync<ProductColor>();
        }
    }
}
