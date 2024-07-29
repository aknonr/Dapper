using Entities.Model;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductService;
using System.Data.SqlClient;

namespace Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("product/{id}", Name = "GetProductByIdAsync")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error while getting product by ID.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting product by ID.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("products", Name = "GetAllProductsAsync")]
        public async Task<ActionResult<List<Product>>> GetAllProductsAsync([FromQuery] DateTime date, [FromQuery] bool isTransferred)
        {
            try
            {
                var result = await _productService.GetAllProductsAsync(date, isTransferred);
                return Ok(result);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error while getting all products.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting all products.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("productcolors", Name = "GetAllProductColorsAsync")]
        public async Task<ActionResult<List<ProductColor>>> GetAllProductColorsAsync([FromQuery] DateTime date, [FromQuery] bool isTransferred)
        {
            try
            {
                var result = await _productService.GetAllProductColorsAsync(date, isTransferred);
                return Ok(result);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error while getting all product colors.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while getting all product colors.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("productcolor/{id}", Name = "GetProductColorByIdAsync")]
        public async Task<ActionResult<ProductColor>> GetProductColorByIdAsync(int id)
        {
            try
            {
                var result = await _productService.GetProductColorByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error while getting product color by ID.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {sqlEx.Message}");
            }

        }

    }
}
