namespace Trading.Web.UI.ViewModels;
public class RegisterModel
{
	[Required(ErrorMessage ="İsim Alanı Gereklidir")]
	public string FirstName { get; set; }
	[Required(ErrorMessage = "Soyİsim Alanı Gereklidir")]
	public string LastName { get; set; }
	[Required(ErrorMessage = "Kullanıcı Adı Gereklidir")]
	public string UserName { get; set; }
	[Required(ErrorMessage = "Şifre Alanı Gereklidir")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	[Required(ErrorMessage = "Şifre Tekrar Alanı Gereklidir")]
	[Compare(nameof(Password))]
	[DataType(DataType.Password)]
	public string ConfirmPassword { get; set; }
	[Required(ErrorMessage = "Email Alanı Gereklidir")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
}