namespace Online.Trading.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly IProductService _productService;
	public ProductController(IProductService productService)
	{
		_productService = productService;
	}
	[HttpGet]
	public async Task<IActionResult> GetProducts()
	{
		var products = await _productService.GetAllAsync();
		return Ok(products);
	}
	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductById(int id)
	{
		var products = await _productService.GetByIdAsync(id);
		if (products == null)
		{
			return NotFound();
		}
		return Ok(products);
	}
	[HttpPost]
	public async Task<IActionResult> CreateProduct(Product model) 
	{
		return Ok(await _productService.CreateAsync(model));
	}
}