namespace Trading.Service.Layer.Concrate;
public class BasketManager : IBasketService
{
	private readonly IBasketRepository _basketRepository;
	public BasketManager(IBasketRepository basketRepository)
	{
		_basketRepository = basketRepository;
	}
	public void AddToBasket(string userId, int productId, int quantity)
	{
		var basket=GetBasketByUserId(userId);
		if (basket != null) 
		{
			var index = basket.BasketItems.FindIndex(x => x.ProductId == productId);
			if (index < 0)
			{
				basket.BasketItems.Add(new BasketItem()
				{
					ProductId = productId,
					Quantity = quantity,
					BasketId = basket.Id,
				});
			}
			else 
			{
				basket.BasketItems[index].Quantity += quantity;
			}
			_basketRepository.Update(basket);
        }
	}

	public void DeleteFromBasket(string userId, int productId)
	{
		var basket = GetBasketByUserId(userId);
		if (basket != null)
		{
			_basketRepository.DeleteFromBasket(basket.Id, productId);
		}
	}
	public Basket GetBasketByUserId(string userId)
	{
		return _basketRepository.GetByUserId(userId);
	}
	public void initialBasket(string userId)
	{
		_basketRepository.Create(new Basket() { userId = userId });
	}
}