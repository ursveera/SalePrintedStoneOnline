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

    }
}
