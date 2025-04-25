using API.eCommerce.Database;
using API.eCommerce.EC;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProductInCart?> Get()
        {
            return new CartEC().Get();
        }

        [HttpGet("{id}")]
        public ProductInCart? GetById(int id)
        {
            return new CartEC().Get().FirstOrDefault(p => p?.item.Id == id);
        }

        [HttpDelete("{id}")]
        public ProductInCart? Delete(int id)
        {
            return new CartEC().Delete(id);
        }

        [HttpPost]
        public ProductInCart? AddOrUpdate([FromBody] ProductInCart product)
        {

            var newItem = new CartEC().AddOrUpdate(product);
            return newItem;
        }

        [HttpPost("clear")]
        public void Clear()
        {
            new CartEC().Clear();
        }
    }
}
