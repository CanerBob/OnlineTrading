namespace Trading.Models.Layer.AllModels;
public class Basket
{
	public int Id { get; set; }
	public string userId { get; set; }
	public List<BasketItem> BasketItems { get; set; }
}