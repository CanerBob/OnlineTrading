namespace Trading.Repository.Layer.EfCore;
public class EfCoreProductRepository : EfCoreGenericRepository<Product, AppDbContext>, IProductRepository
{
	public Product GetByIdWithCategories(int id)
	{
		using (var context = new AppDbContext())
		{
			return context.Products
					.Where(p => p.Id == id)
					.Include(x => x.ProductCategories)
					.ThenInclude(x => x.Category)
					.FirstOrDefault();
		}
	}
	public int GetCountByCategory(string category)
	{
		using (var context = new AppDbContext())
		{
			var products = context.Products.Where(x => x.IsApproved).AsQueryable();
			if (!string.IsNullOrEmpty(category))
			{
				products = products
						.Include(x => x.ProductCategories)
						.ThenInclude(x => x.Category)
						.Where(x => x.ProductCategories.Any(x => x.Category.Url == category));
			}
			return products.Count();
		}
	}
	public List<Product> GetHomePageProducts()
	{
		using var context = new AppDbContext();
		return context.Products.Where(x => x.IsHome && x.IsApproved).ToList();
	}
	public Product GetProductDetails(string url)
	{
		using var context = new AppDbContext();
		return context.Products
			.Where(x => x.Url == url)
			.Include(x => x.ProductCategories)
			.ThenInclude(x => x.Category)
			.FirstOrDefault();
	}
	public List<Product> GetProductsByCategory(string name, int page, int pageSize)
	{
		using (var context = new AppDbContext())
		{
			var products = context
				.Products
				.Where(i => i.IsApproved)
				.AsQueryable();

			if (!string.IsNullOrEmpty(name))
			{
				products = products
								.Include(i => i.ProductCategories)
								.ThenInclude(i => i.Category)
								.Where(i => i.ProductCategories
								.Any(a => a.Category.Url == name));
			}

			return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}
	}
	public List<Product> GetSearchResult(string word)
	{
		using (var context = new AppDbContext())
		{
			var products = context
				.Products
				.Where(x => x.IsApproved && (x.Name.Contains(word)) || x.Description.Contains(word))
				.AsQueryable();

			return products.ToList();
		}

	}
	public void Update(Product entity, int[] categoryIds)
	{
		using (var context = new AppDbContext())
		{
			var product = context.Products
					.Include(x => x.ProductCategories)
					.FirstOrDefault(x => x.Id == entity.Id);
			if (product != null)
			{
				product.Name = entity.Name;
				product.Description = entity.Description;
				product.Price = entity.Price;
				product.ImageUrl = entity.ImageUrl;
				product.Url = entity.Url;
				product.IsApproved = entity.IsApproved;
				product.IsHome = entity.IsHome;
				product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
				{
					ProductId = entity.Id,
					CategoryId = catid
				}).ToList();
			}
			context.SaveChanges();
		};
	}
}