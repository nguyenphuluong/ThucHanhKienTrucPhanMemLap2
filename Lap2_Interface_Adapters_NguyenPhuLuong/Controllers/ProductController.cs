using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Services;

namespace ProductManagement.InterfaceAdapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await _service.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(CreateProductDTO productDto)
        {
            var product = await _service.AddProductAsync(productDto);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductDTO productDto)
        {
            if (id != productDto.Id)
                return BadRequest();

            await _service.UpdateProductAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
