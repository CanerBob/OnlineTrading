namespace Trading.Web.UI.ViewModels;
public class ResetPasswordModel
{
    [Required]
    public string Token { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}