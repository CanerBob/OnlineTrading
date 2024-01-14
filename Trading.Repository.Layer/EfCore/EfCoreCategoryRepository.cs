namespace Trading.Repository.Layer.EfCore;
public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, AppDbContext>, ICategoryRepository
{
	public void DeleteFromCategory(int pid, int cid)
	{
		using (var context = new AppDbContext()) 
		{
			var cmd = "delete from ProductCategories where ProductId=@p0 and CategoryId=@p1";
			context.Database.ExecuteSqlRaw(cmd, pid, cid);
		};
	}
	public Category GetByIdWithProducts(int id)
	{
		using (var context = new AppDbContext()) 
		{
			return context.Categories
				   .Where(x => x.Id == id)
				   .Include(x => x.ProductCategories)
				   .ThenInclude(x => x.Product)
				   .FirstOrDefault();
		};
		
	}
}