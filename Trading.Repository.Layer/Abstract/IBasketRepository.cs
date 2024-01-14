namespace Trading.Repository.Layer.Abstract;
public interface IBasketRepository:IGenericRepository<Basket>
{
	Basket GetByUserId(string userId);
	void DeleteFromBasket(int basketId, int productId);
}