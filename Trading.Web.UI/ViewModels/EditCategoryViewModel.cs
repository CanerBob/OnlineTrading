namespace Trading.Web.UI.ViewModels;
public class EditCategoryViewModel
{
	public int CategoryId { get; set; }
	[Required(ErrorMessage ="Kategori İsmi Zorunludur")]
	[StringLength(20,MinimumLength =5,ErrorMessage ="Kategori Alanı İçin 5 ile 20 Karakter Arasında Bir Değer Olmalıdır")]
	public string Name { get; set; }
	public string Url { get; set; }
	public List<Product> Products { get; set; }
}