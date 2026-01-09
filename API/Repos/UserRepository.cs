using DAO.DTO;
using SqlKata.Execution;
namespace API.Repos
{
    using DAO.Models;
    using SqlKata.Execution;

    public class UserRepository : IUserRepository
    {
        private readonly QueryFactory _db;

        public UserRepository(QueryFactory db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(User user)
        {
            return await _db.Query("Users")
                .InsertGetIdAsync<int>(new
                {
                    user.Name,
                    user.Phone,
                    user.Email,
                    user.Address,
                    CreatedAt = DateTime.UtcNow,
                    user.isActive,
                });
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _db.Query("Users")
                .Where("UserId", userId)
                .FirstOrDefaultAsync<User>();
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _db.Query("Users")
                .Where("Phone", phone)
                .FirstOrDefaultAsync<User>();
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await _db.Query("Users")
                .Where("Phone", phone)
                .ExistsAsync();
        }
        public async Task<bool> UpdateAsync(int userId, User user)
        {
            var affected = await _db.Query("Users")
                .Where("UserId", userId)
                .Where("IsActive", true)
                .UpdateAsync(new
                {
                    user.Name,
                    user.Phone,
                    user.Email,
                    user.Address,
                });

            return affected > 0;
        }

        // ❌ DELETE USER (Soft Delete)
        public async Task<bool> DeleteAsync(int userId)
        {
            var affected = await _db.Query("Users")
                .Where("UserId", userId)
                .Where("IsActive", true)
                .UpdateAsync(new
                {
                    IsActive = false
                });

            return affected > 0;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _db.Query("Users")
                .Where("IsActive", true)          // exclude deleted users
                .OrderByDesc("CreatedAt")         // latest first
                .GetAsync<User>();

            return users.ToList();
        }
    }

}
