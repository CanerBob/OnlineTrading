namespace Trading.Service.Layer.Abstract;
public interface IBasketService
{
	void initialBasket(string userId);
	Basket GetBasketByUserId(string userId);
	void AddToBasket(string userId, int productId,int quantity);
	void DeleteFromBasket(string userId, int productId);
}