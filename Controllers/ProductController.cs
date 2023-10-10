using AutoMapper;
using Backend_Task.Entities;
using Backend_Task.Models.Product;
using Backend_Task.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork hotels, IMapper mapper)
        {
            _unitOfWork = hotels;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProduct>>> GetProducts()
        {
            var products = await _unitOfWork.productRepository.GetAll<GetProduct>();
            return Ok(products);
        }

        
        [HttpGet("{productCode}")]
        public async Task<ActionResult<GetProduct>> GetProduct(string productCode)
        {
            var product = await _unitOfWork.productRepository.Get<GetProduct>(productCode);


            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{productCode}")]
        public async Task<IActionResult> PutProduct(string productCode, [FromForm]UpdateProduct updateProduct)
        {
           
            await _unitOfWork.productRepository.Update(productCode, updateProduct);
            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm]CreateProduct createProduct)
        {
            
            var Product=await _unitOfWork.productRepository.Add<CreateProduct,Product>(createProduct);
  

            return CreatedAtAction("GetProduct", new {productCode=createProduct.ProductCode},createProduct);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{productCode}")]
        public async Task<IActionResult> DeleteProduct(string productCode)
        {
            await _unitOfWork.productRepository.Delete(productCode);
            return NoContent();
        }
    }
}
