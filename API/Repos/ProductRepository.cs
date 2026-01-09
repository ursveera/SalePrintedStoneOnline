using DAO.DTO;
using SqlKata.Execution;

namespace API.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly QueryFactory _db;

        public ProductRepository(QueryFactory db)
        {
            _db = db;
        }

        public async Task<ApiResponse<int>> CreateProductAsync(CreateProductRequest request)
        {
            _db.Connection.Open();

            try
            {
                var productId = await _db.Query("Products")
                    .InsertGetIdAsync<int>(new
                    {
                        request.Name,
                        request.Description,
                        request.BasePrice,
                        request.DefaultImageUrl,
                        request.IsActive,
                        CreatedAt = DateTime.UtcNow
                    });

                if (request.Colors != null)
                {
                    foreach (var color in request.Colors)
                    {
                        await _db.Query("ProductColors")
                            .InsertAsync(new
                            {
                                ProductId = productId,
                                color.ColorName,
                                color.ExtraPrice,
                                color.ImageUrl
                            });
                    }
                }

                if (request.Images != null)
                {
                    foreach (var imageUrl in request.Images)
                    {
                        await _db.Query("ProductImages")
                            .InsertAsync(new
                            {
                                ProductId = productId,
                                ImageUrl = imageUrl
                            });
                    }
                }


                return new ApiResponse<int>
                {
                    success = true,
                    message = "Product created successfully",
                    data = productId
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    success = false,
                    message = ex.Message,
                    data = 0
                };
            }
            finally
            {
                _db.Connection.Close();
            }
        }

        public async Task<ApiResponse<ProductResponse>> GetProductAsync(int productId)
        {
            try
            {
                var product = await _db.Query("Products")
                    .Where("ProductId", productId)
                    .FirstOrDefaultAsync<ProductResponse>();

                if (product == null)
                {
                    return new ApiResponse<ProductResponse>
                    {
                        success = false,
                        message = "Product not found",
                        data = null
                    };
                }

                product.Colors = (await _db.Query("ProductColors")
                    .Where("ProductId", productId)
                    .GetAsync<ProductColorResponse>())
                    .ToList();

                product.Images = (await _db.Query("ProductImages")
                    .Where("ProductId", productId)
                    .Select("ImageUrl")
                    .GetAsync<string>())
                    .ToList();

                return new ApiResponse<ProductResponse>
                {
                    success = true,
                    message = "Product fetched successfully",
                    data = product
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponse>
                {
                    success = false,
                    message = ex.Message,
                    data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateProductAsync(int productId, CreateProductRequest request)
        {
            _db.Connection.Open();

            try
            {
                var updated = await _db.Query("Products")
                    .Where("ProductId", productId)
                    .UpdateAsync(new
                    {
                        request.Name,
                        request.Description,
                        request.BasePrice,
                        request.DefaultImageUrl,
                        request.IsActive
                    });

                if (updated == 0)
                {
                    return new ApiResponse<bool>
                    {
                        success = false,
                        message = "Product not found",
                        data = false
                    };
                }

                await _db.Query("ProductColors")
                    .Where("ProductId", productId)
                    .DeleteAsync();

                await _db.Query("ProductImages")
                    .Where("ProductId", productId)
                    .DeleteAsync();

                if (request.Colors != null)
                {
                    foreach (var color in request.Colors)
                    {
                        await _db.Query("ProductColors")
                            .InsertAsync(new
                            {
                                ProductId = productId,
                                color.ColorName,
                                color.ExtraPrice,
                                color.ImageUrl
                            });
                    }
                }

                if (request.Images != null)
                {
                    foreach (var imageUrl in request.Images)
                    {
                        await _db.Query("ProductImages")
                            .InsertAsync(new
                            {
                                ProductId = productId,
                                ImageUrl = imageUrl
                            });
                    }
                }


                return new ApiResponse<bool>
                {
                    success = true,
                    message = "Product updated successfully",
                    data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    success = false,
                    message = ex.Message,
                    data = false
                };
            }
            finally
            {
                _db.Connection.Close();
            }
        }

        public async Task<ApiResponse<bool>> DeleteProductAsync(int productId)
        {
            _db.Connection.Open();

            try
            {
                await _db.Query("ProductColors")
                    .Where("ProductId", productId)
                    .DeleteAsync();

                await _db.Query("ProductImages")
                    .Where("ProductId", productId)
                    .DeleteAsync();

                var deleted = await _db.Query("Products")
                    .Where("ProductId", productId)
                    .DeleteAsync();


                return new ApiResponse<bool>
                {
                    success = deleted > 0,
                    message = deleted > 0 ? "Product deleted successfully" : "Product not found",
                    data = deleted > 0
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    success = false,
                    message = ex.Message,
                    data = false
                };
            }
            finally
            {
                _db.Connection.Close();
            }
        }
    }
}
