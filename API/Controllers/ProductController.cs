using API.Repos;
using DAO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // 🔹 CREATE PRODUCT
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>
                {
                    success = false,
                    message = "Invalid request data",
                    data = null
                });

            var result = await _productRepo.CreateProductAsync(request);

            return result.success
                ? Ok(result)
                : BadRequest(result);
        }

        // 🔹 GET PRODUCT BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productRepo.GetProductAsync(id);

            return result.success
                ? Ok(result)
                : NotFound(result);
        }

        // 🔹 UPDATE PRODUCT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>
                {
                    success = false,
                    message = "Invalid request data",
                    data = null
                });

            var result = await _productRepo.UpdateProductAsync(id, request);

            return result.success
                ? Ok(result)
                : NotFound(result);
        }

        // 🔹 DELETE PRODUCT
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepo.DeleteProductAsync(id);

            return result.success
                ? Ok(result)
                : NotFound(result);
        }
    }
}

