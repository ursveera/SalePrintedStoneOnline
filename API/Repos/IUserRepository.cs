using DAO.DTO;
using DAO.Models;
using SqlKata.Execution;

namespace API.Repos
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);
        Task<User?> GetByIdAsync(int userId);
        Task<User?> GetByPhoneAsync(string phone);
        Task<bool> ExistsByPhoneAsync(string phone);

        Task<List<User>> GetAllUsersAsync();
        Task<bool> UpdateAsync(int userId, User user);
        Task<bool> DeleteAsync(int userId);
    }
}
