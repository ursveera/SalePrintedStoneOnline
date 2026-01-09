using API.Repos;
using DAO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // ===============================
        // CREATE USER
        // POST: api/users
        // ===============================
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check duplicate phone
            if (await _userRepo.ExistsByPhoneAsync(user.Phone))
            {
                var existing = await _userRepo.GetByPhoneAsync(user.Phone);
                return Ok(new
                {
                    success = true,
                    message = "User already exists",
                    data = existing
                });
            }

            var userId = await _userRepo.CreateAsync(user);
            var createdUser = await _userRepo.GetByIdAsync(userId);

            return Ok(new
            {
                success = true,
                message = "User created successfully",
                data = createdUser
            });
        }

        // ===============================
        // GET USER BY ID
        // GET: api/users/{id}
        // ===============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // ===============================
        // GET ALL USERS
        // GET: api/users
        // ===============================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return Ok(users);
        }

        // ===============================
        // UPDATE USER
        // PUT: api/users/{id}
        // ===============================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Prevent duplicate phone
            var existing = await _userRepo.GetByPhoneAsync(user.Phone);
            if (existing != null && existing.UserId != id)
                return BadRequest("Phone number already exists");

            var updated = await _userRepo.UpdateAsync(id, user);
            if (!updated)
                return NotFound("User not found");

            return Ok(new
            {
                success = true,
                message = "User updated successfully"
            });
        }

        // ===============================
        // DELETE USER (Soft Delete)
        // DELETE: api/users/{id}
        // ===============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userRepo.DeleteAsync(id);
            if (!deleted)
                return NotFound("User not found");

            return Ok(new
            {
                success = true,
                message = "User deleted successfully"
            });
        }
    }
}
