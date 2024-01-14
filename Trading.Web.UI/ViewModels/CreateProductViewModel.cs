namespace Trading.Web.UI.ViewModels;
public class CreateProductViewModel
{
	public string Name { get; set; }
	public string Url { get; set; }
	public double? Price { get; set; }
	public string Description { get; set; }
	public string ImageUrl { get; set; }
	public bool IsApproved { get; set; }
	public bool IsHome { get; set; }
}