namespace Trading.Web.UI.Controllers;
public class AccessController : Controller
{
	public IActionResult AccesDenied()
	{
		return View();
	}
}
