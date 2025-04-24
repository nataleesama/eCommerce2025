using API.eCommerce.EC;
using Library.eCommerce.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.eCommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{

    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<ProductDTO?> Get()
    {
        return new InventoryEC().Get();
    }

    [HttpGet("{id}")]
    public ProductDTO? GetById(int id)
    {
        return new InventoryEC().Get().FirstOrDefault(p => p?.Id == id);
    }

    [HttpDelete("{id}")]
    public ProductDTO? Delete(int id)
    {
        return new InventoryEC().Delete(id);
    }
}
