﻿namespace Trading.Web.UI.Controllers;
public class ShopController : Controller
{
    private IProductService _productService;
    public ShopController(IProductService productService)
    {
       this._productService = productService;
    }
    public IActionResult List(string category, int page = 1)
    {
        const int pageSize = 3;
        var productViewModel = new ProductListViewModel()
        {
            PageInfo = new PageInfo()
            {
                TotalItems = _productService.GetCountByCategory(category),
                CurrentPage = page,
                ItemsPerPage = pageSize,
                CurrentCategory = category
            },
            Products = _productService.GetProductsByCategory(category, page, pageSize)
        };
        return View(productViewModel);
    }
    public IActionResult Details(string url)
    {
        if (url == null)
        {
            return NotFound();
        }
        Product product = _productService.GetProductDetails(url);
        if (product == null)
        {
            return NotFound();
        }
        return View(new ProductDetailsViewModel
        {
            Product = product,
            Categories = product.ProductCategories.Select(x => x.Category).ToList()
        });
    }
    public IActionResult Search(string q)
    {
        var productViewModel = new ProductListViewModel()
        {
            Products = _productService.GetSearchResult(q)
        };
        return View(productViewModel);
    }
}