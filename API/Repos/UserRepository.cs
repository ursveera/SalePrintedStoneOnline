using DAO.DTO;
using SqlKata.Execution;
namespace API.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly QueryFactory _db;

        public UserRepository(QueryFactory db)
        {
            _db = db;
        }

        // this is for get all users
        public async Task<List<AdminUserDto>> GetAllUsersAsync()
        {
            var users = await _db.Query("Users as u")
                .LeftJoin("Orders as o", "u.UserId", "o.UserId")
                .Select(
                    "u.UserId",
                    "u.Name",
                    "u.Phone",
                    "u.Email"
                )
                .SelectRaw("COUNT(o.OrderId) as TotalOrders")
                .GroupBy("u.UserId", "u.Name", "u.Phone", "u.Email")
                .OrderByDesc("u.UserId")
                .GetAsync<AdminUserDto>();

            return users.ToList();
        }

        // 📦 Admin: Get orders by user
        public async Task<List<AdminOrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _db.Query("Orders as o")
                .Join("Users as u", "o.UserId", "u.UserId")
                .Join("ProductColors as c", "o.ColorId", "c.ColorId")
                .Where("o.UserId", userId)
                .Select(
                    "o.OrderId",
                    "u.Name as CustomerName",
                    "c.ColorName",
                    "o.PrintedLabel",
                    "o.TotalPrice",
                    "o.OrderStatus",
                    "o.CreatedAt as OrderDate"
                )
                .OrderByDesc("o.CreatedAt")
                .GetAsync<AdminOrderDto>();

            return orders.ToList();
        }

        // 🔄 Admin: Update order status
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var affected = await _db.Query("Orders")
                .Where("OrderId", orderId)
                .UpdateAsync(new
                {
                    OrderStatus = status
                });

            return affected > 0;
        }
    }

}
