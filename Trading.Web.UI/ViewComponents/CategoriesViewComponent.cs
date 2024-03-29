﻿namespace Trading.Web.UI.ViewComponents;
public class CategoriesViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;
    public CategoriesViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public IViewComponentResult Invoke()
    {
        if (RouteData.Values["category"]!= null)
            ViewBag.SelectedCategory = RouteData?.Values["category"];
        return View(_categoryService.GetAll());
    }
}