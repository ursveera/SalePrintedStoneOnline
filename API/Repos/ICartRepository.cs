using DAO.DTO;
using DAO.Models;

namespace API.Repos
{
    public interface ICartRepository
    {
        Task<Cart> GetOrCreateCartAsync(string sessionId);
        Task AddItemAsync(int cartId, AddToCartRequest request);
        Task<IEnumerable<object>> GetCartItemsAsync(int cartId);
    }
}
