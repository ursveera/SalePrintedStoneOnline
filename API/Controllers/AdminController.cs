using API.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/SalePrintedStone/[Controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public AdminController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
            => Ok(await _repo.GetAllUsersAsync());

        [HttpGet("users/{userId}/orders")]
        public async Task<IActionResult> GetUserOrders(int userId)
            => Ok(await _repo.GetOrdersByUserIdAsync(userId));

        [HttpPut("orders/{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
            => Ok(await _repo.UpdateOrderStatusAsync(orderId, status));
    }
}
