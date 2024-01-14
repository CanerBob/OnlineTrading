namespace Trading.Web.UI.Controllers;
[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
	private readonly IProductService _productService;
	private readonly ICategoryService _categoryService;
	public AdminController(IProductService productService, ICategoryService categoryService)
	{
		_productService = productService;
		_categoryService = categoryService;
	}
	public IActionResult ForAdmin()
	{
		return View();
	}
	public async Task<IActionResult> ProductList()
	{
		return View(new ProductListViewModel()
		{
			Products = await _productService.GetAllAsync()
		}); ;
	}
	[HttpGet]
	public IActionResult CreateProduct()
	{
		return View();
	}
	[HttpPost]
	public IActionResult CreateProduct(CreateProductViewModel model)
	{
		if (ModelState.IsValid)
		{
			Product product = new Product()
			{
				Name = model.Name,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				Url = model.Url,
				IsApproved = false,
				IsHome = false
			};
			if (_productService.Create(product))
			{
				CreateMessage($"{model.Name} İsimli Ürün Eklendi", "success");
				return RedirectToAction(nameof(ProductList));
			}
			CreateMessage(_productService.ErrorMessage, "danger");
			return View(model);
		}
		return View(model);
	}
	[HttpGet]
	public IActionResult EditProduct(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}
		var entity = _productService.GetByIdWithCategories((int)id);
		if (entity == null)
		{
			return NotFound();
		}
		var item = new EditProductViewModel()
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			ImageUrl = entity.ImageUrl,
			Price = entity.Price,
			Url = entity.Url,
			IsApproved = entity.IsApproved,
			IsHome = entity.IsHome,
			SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList()
		};
		ViewBag.categories = _categoryService.GetAll();
		return View(item);
	}
	[HttpPost]
	public async Task<IActionResult> EditProduct(EditProductViewModel model, int[] categoryIds, IFormFile file)
	{
		var entity = await _productService.GetByIdAsync(model.Id);
		if (entity == null)
		{
			return NotFound();
		}
		entity.Name = model.Name;
		entity.Description = model.Description;
		entity.Price = model.Price;
		entity.Url = model.Url;
		entity.IsApproved = model.IsApproved;
		entity.IsHome = model.IsHome;
		if (file != null)
		{
			var extension = Path.GetExtension(file.FileName);
			var randomName = string.Format($"{Guid.NewGuid}{extension}");
			entity.ImageUrl = randomName;
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
		}
		if (_productService.Update(entity, categoryIds))
		{
			CreateMessage($"{model.Name} İsimli Ürün Güncellendi", "success");
			return RedirectToAction(nameof(ProductList));
		}
		CreateMessage(_productService.ErrorMessage, "danger");
		ViewBag.categories = _categoryService.GetAll();
		return View(model);
	}
	public async Task<IActionResult> DeleteProduct(int id)
	{
		var entity = await _productService.GetByIdAsync(id);
		if (entity != null)
		{
			_productService.Delete(entity);
			var msg = new AlertMessage
			{
				Message = $"{entity.Name} İsimli Ürün Silinmiştir",
				AlertType = "danger"
			};
			TempData["message"] = JsonConvert.SerializeObject(msg);

		}
		return RedirectToAction(nameof(ProductList));

	}
	private void CreateMessage(string message, string allerttype)
	{
		var msg = new AlertMessage
		{
			Message = message,
			AlertType = allerttype
		};
		TempData["message"] = JsonConvert.SerializeObject(msg);
	}
}