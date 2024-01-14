namespace Trading.Web.UI.ViewModels;
public class BasketModel
{
	public int BasketId { get; set; }
    public List<BasketItemModel> BasketItems { get; set; }
    public double TotalPrice() 
    {
        return BasketItems.Sum(x => x.Price * x.Quantity);
    }
}
public class BasketItemModel
{
    public int BasketItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }
}