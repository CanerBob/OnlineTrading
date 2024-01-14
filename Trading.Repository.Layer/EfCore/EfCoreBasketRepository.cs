namespace Trading.Repository.Layer.EfCore;
public class EfCoreBasketRepository : EfCoreGenericRepository<Basket, AppDbContext>, IBasketRepository
{
	public override void Update(Basket entity)
	{
		using (var context = new AppDbContext())
		{
			context.Basket.Update(entity);
			context.SaveChanges();
		}
	}
	public Basket GetByUserId(string userId)
	{
		using (var context = new AppDbContext()) 
		{
			return context.Basket
					.Include(x => x.BasketItems)
					.ThenInclude(x => x.Product)
					.Where(x => x.userId == userId)
					.FirstOrDefault();
		}
	}
	public void DeleteFromBasket(int basketId, int productId)
	{
		using (var context = new AppDbContext()) 
		{
			var cmd = @"delete from BasketItems where BasketId=@p0 and ProductId=@p1";
			context.Database.ExecuteSqlRaw(cmd, basketId, productId);
		}
	}
}