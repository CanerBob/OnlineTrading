namespace Trading.Web.UI.ViewModels;
public class EditProductViewModel
{
    public int Id { get; set; }
	[Required(ErrorMessage = "İsim Alanı Zorunludur")]
	public string Name { get; set; }
	[Required(ErrorMessage = "Url Alanı Zorunludur")]
	public string Url { get; set; }
	[Required(ErrorMessage = "Price Alanı Zorunludur")]
	[Range(1, 100000, ErrorMessage = "Fiyat Alanı İçin 1 ile 100000 arasında bir değer girmelisiniz")]
	public double? Price { get; set; }
	[Required(ErrorMessage = "Description Alanı Zorunludur")]
	[StringLength(1000, MinimumLength = 5, ErrorMessage = "Açıklama için 5 ile 1000 Karakter arasında değer girmelisiniz")]
	public string Description { get; set; }
	[Required(ErrorMessage = "ImageUrl Alanı Zorunludur")]
	public string ImageUrl { get; set; }
	public bool IsApproved { get; set; }
	public bool IsHome { get; set; }
	public List<Category> SelectedCategories { get; set; }
}