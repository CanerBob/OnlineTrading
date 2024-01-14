namespace Trading.Web.UI.Controllers;
[Authorize]
public class CategoryController : Controller
{
	private readonly ICategoryService _categoryService;
	public CategoryController(ICategoryService categoryService)
	{
		_categoryService = categoryService;
	}
	public async Task<IActionResult> CategoryList()
	{
		return View(new CategoryListViewModel()
		{
			Categories = await _categoryService.GetAllAsync()
		}); ;
	}
	[HttpGet]
	public IActionResult CreateCategory()
	{
		return View();
	}
	[HttpPost]
	public IActionResult CreateCategory(CategoryModel model)
	{
		if (ModelState.IsValid)
		{
			Category category = new Category()
			{
				Name = model.Name,
				Url = model.Url
			};
			_categoryService.Create(category);
			var msg = new AlertMessage
			{
				Message = $"{category.Name} İsimli Kategori Eklenmiştir",
				AlertType = "success"
			};
			TempData["message"] = JsonConvert.SerializeObject(msg);
			return RedirectToAction(nameof(CategoryList));
		}
		return View(model);
	}
	[HttpGet]
	public IActionResult EditCategory(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}
		var entity = _categoryService.GetByIdWithProducts((int)id);
		if (entity == null)
		{
			return NotFound();
		}
		var item = new EditCategoryViewModel()
		{
			CategoryId = entity.Id,
			Name = entity.Name,
			Url = entity.Url,
			Products = entity.ProductCategories.Select(x => x.Product).ToList()
		};
		return View(item);
	}
	[HttpPost]
	public IActionResult EditCategory(EditCategoryViewModel model)
	{
		if (ModelState.IsValid)
		{
			var category = _categoryService.GetById(model.CategoryId);
			if (category == null)
			{
				return NotFound();
			}
			category.Name = model.Name;
			category.Url = model.Url;
			_categoryService.Update(category);
			var msg = new AlertMessage
			{
				Message = $"{category.Name} İsimli Ürün Güncellenmiştir",
				AlertType = "info"
			};
			TempData["message"] = JsonConvert.SerializeObject(msg);
			return RedirectToAction(nameof(CategoryList));
		}
		return View(model);
	}
	public async Task<IActionResult> DeleteCategory(int id)
	{
		var item = await _categoryService.GetByIdAsync(id);
		if (item != null)
		{
			_categoryService.Delete(item);
			var msg = new AlertMessage
			{
				Message = $"{item.Name} İsimli Ürün Silinmiştir",
				AlertType = "danger"
			};
			TempData["message"] = JsonConvert.SerializeObject(msg);
		}
		return RedirectToAction(nameof(CategoryList));
	}
	[HttpPost]
	public IActionResult DeleteFromCategory(int Id, int categoryId)
	{
		_categoryService.DeleteFromCategory(Id, categoryId);
		return RedirectToAction(nameof(CategoryList));
	}
}