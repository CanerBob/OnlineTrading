namespace Trading.Web.UI.Controllers;
public class HomeController : Controller
{
    private readonly IProductService _productService;
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
    public IActionResult Index()
    {
        var productviewmodel = new ProductListViewModel()
        {
            Products = _productService.GetHomePageProducts()
        };
        return View(productviewmodel);
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }
}
