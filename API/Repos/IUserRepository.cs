using DAO.DTO;
using SqlKata.Execution;

namespace API.Repos
{
    public interface IUserRepository
    {
        Task<List<AdminUserDto>> GetAllUsersAsync();
        Task<List<AdminOrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}
