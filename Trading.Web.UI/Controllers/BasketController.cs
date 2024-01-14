namespace Trading.Web.UI.Controllers;
[Authorize]
public class BasketController : Controller
{
	private readonly IBasketService _basketService;
	private readonly UserManager<Person> _userManager;
	public BasketController(IBasketService basketService, UserManager<Person> userManager)
	{
		_basketService = basketService;
		_userManager = userManager;
	}
	public IActionResult Index()
	{
		var basket = _basketService.GetBasketByUserId(_userManager.GetUserId(User));
		return View(new BasketModel()
		{
			BasketId = basket.Id,
			BasketItems = basket.BasketItems.Select(x => new BasketItemModel()
			{
				BasketItemId = x.Id,
				ProductId = x.ProductId,
				ProductName=x.Product.Name,
				ImageUrl=x.Product.ImageUrl,
				Price=(double)x.Product.Price,
				Quantity=x.Quantity
			}).ToList()
		}) ; 
	}
	[HttpPost]
	public IActionResult AddToBasket(int productId,int quantity) 
	{
		var userId=_userManager.GetUserId(User);
		_basketService.AddToBasket (userId,productId, quantity);
		return Redirect(nameof(Index));
	}
	[HttpPost]
	public IActionResult DeleteFromBasket(int productId) 
	{
		var userId= _userManager.GetUserId(User);
		_basketService.DeleteFromBasket(userId,productId);
		return Redirect(nameof(Index));
	}
}