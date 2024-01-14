using NuGet.Common;

namespace Trading.Web.UI.Controllers;
public class AccountController : Controller
{
	private readonly UserManager<Person> _userManager;
	private readonly SignInManager<Person> _signInManager;
	private readonly IEmailSender _emailSender;
	private readonly IBasketService _basketService;
	public AccountController(UserManager<Person> userManager, SignInManager<Person> signInManager, IEmailSender emailSender, IBasketService basketService)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_emailSender = emailSender;
		_basketService = basketService;
	}
	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}
	public async Task<IActionResult> LogOut()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
		{
			ModelState.AddModelError("", "Bu Mail Adresine Ait Kayıtlı Kullanıcı Yok");
			return View(model);
		}
		if (!await _userManager.IsEmailConfirmedAsync(user))
		{
			ModelState.AddModelError("", "Lütfen Hesabınızı Onaylayın");
			return View(model);
		}
		var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
		if (result.Succeeded)
		{
			return RedirectToAction("Index", "Home");
		}
		ModelState.AddModelError("", "Kullanıcı Adı veya Parola Yanlış");
		return View(model);
	}
	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}
		var person = new Person()
		{
			FirstName = model.FirstName,
			LastName = model.LastName,
			Email = model.Email,
			UserName = model.UserName,
		};
		var result = await _userManager.CreateAsync(person, model.Password);
		if (result.Succeeded)
		{
			//token oluşturulacak
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(person);
			var url = Url.Action("Account","ConfirmEmail" , new
			{
				userId = person.Id,
				token = code
			});
			await _emailSender.SendEmailAsync(model.Email, "Hesabınız Onaylayınız", $"<a href='https://localhost:7167{url}'>Lütfen Email Hesabınızı Onaylamak için Linke Tıklayın<a/>");
			return RedirectToAction(nameof(Login));
		}
		return View(model);
	}
	public async Task<IActionResult> ConfirmEmail(string userId, string Token)
	{
		if (userId == null || Token == null)
		{
			
			return View();
		}
		var user = await _userManager.FindByIdAsync(userId);
		if (user != null)
		{
			var result = await _userManager.ConfirmEmailAsync(user, Token);
			if (result.Succeeded)
			{
				// cart objesini oluştur.
				_basketService.initialBasket(user.Id);

			
				return View();
			}
		}
		
		return View();
	}
	public IActionResult ForgotPassword()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> ForgotPassword(string Email)
	{
		if (string.IsNullOrEmpty(Email))
		{
			return View();
		}
		var user= await _userManager.FindByEmailAsync(Email);
		if (user == null) 
		{
			CreateMessage("Böyle Bir Email Yok", "Warning");
			return View();
		}
		var code= await _userManager.GeneratePasswordResetTokenAsync(user);
		var url = Url.Action("ResetPassword", "Account", new
		{
			userId = user.Id,
			token = code
		});
		await _emailSender.SendEmailAsync(Email, "Şifrenizi Yenileyin", $"<a href='https://localhost:7167{url}'>Lütfen Email Hesabınızı Yenilemek için Linke Tıklayın<a/>");
		return View();
	}
	public IActionResult ResetPassword(string userId,string token)
	{
		if (User == null || token == null) 
		{
			return RedirectToAction("Index", "Home");
		}
		var model = new ResetPasswordModel { Token = token };
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
	{
		if (!ModelState.IsValid) 
		{
			return View(model);
		}
		var user= await _userManager.FindByEmailAsync(model.Email);
		if (user == null) 
		{
			return RedirectToAction("Index", "Home");
		}
		var result=await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
		if (result.Succeeded) 
		{
			return RedirectToAction(nameof(Login));
		}
		return View(model);
	}
	private void CreateMessage(string message, string allerttype)
	{
		var msg = new AlertMessage
		{
			Message = message,
			AlertType = allerttype
		};
		TempData["message"] = JsonConvert.SerializeObject(msg);
	}
}