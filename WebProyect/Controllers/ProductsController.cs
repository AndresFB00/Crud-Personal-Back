using Microsoft.AspNetCore.Mvc;
using WebProyect.Data;
using WebProyect.Models;
using Microsoft.EntityFrameworkCore;

namespace WebProyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : ""
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : ""
                })
                .FirstOrDefaultAsync();

            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Cargar la categoría para el DTO
            var category = await _context.Categories.FindAsync(product.CategoryId);

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = category != null ? category.Name : ""
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            var category = await _context.Categories.FindAsync(product.CategoryId);

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = category != null ? category.Name : ""
            };

            return Ok(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            // Prepara el DTO antes de eliminar
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : ""
            };

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(productDto);
        }
    }
}
