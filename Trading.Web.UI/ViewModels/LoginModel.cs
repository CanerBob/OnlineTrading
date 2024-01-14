namespace Trading.Web.UI.ViewModels;
public class LoginModel
{
    [Required(ErrorMessage ="Mail Alanı Gereklidir")]
	[DataType(DataType.EmailAddress)]
    public string Email { get; set; }
	[Required(ErrorMessage ="Şifre Alanı Gereklidir")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
}