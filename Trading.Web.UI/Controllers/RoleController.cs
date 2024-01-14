namespace Trading.Web.UI.Controllers;
[Authorize]
public class RoleController : Controller
{
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<Person> _userManager;
	public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
	{
		_roleManager = roleManager;
		_userManager = userManager;
	}
	public IActionResult UserList()
	{
		return View(_userManager.Users);
	}
	public async Task<IActionResult> EditUser(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user != null)
		{
			var selectedRoles = await _userManager.GetRolesAsync(user);
			var roles = _roleManager.Roles.Select(x => x.Name);
			ViewBag.Roles = roles;
			return View(new UserEditViewModel()
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				Email = user.Email,
				EmailConfirmed = user.EmailConfirmed,
				SelectedRoles = selectedRoles
			});
		}
		return RedirectToAction(nameof(UserList));
	}
	[HttpPost]
	public async Task<IActionResult> EditUser(UserEditViewModel model, string[] selectedRoles)
	{
		
			var user = await _userManager.FindByIdAsync(model.UserId);
			if (user != null)
			{
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.UserName = model.UserName;
				user.Email = model.Email;
				user.EmailConfirmed = model.EmailConfirmed;
				var result=	await _userManager.UpdateAsync(user);
				if (result.Succeeded) 
				{
				var userRoles=await _userManager.GetRolesAsync(user);
					selectedRoles = selectedRoles ?? new string[] { };
					await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
					await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
					return RedirectToAction(nameof(UserList));
				}
			}
			return View(model);
		
		
	}
	public IActionResult RoleList()
	{
		return View(_roleManager.Roles);
	}
	[HttpGet]
	public async Task<IActionResult> RoleEdit(string id)
	{
		var role = await _roleManager.FindByIdAsync(id);
		var members = new List<Person>();
		var nonMembers = new List<Person>();
		//Bütün kullanıcıları almak mantıklı bir çözüm değil!!!!
		foreach (var user in _userManager.Users)
		{
			var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
			list.Add(user);
		}
		var model = new RoleDetails()
		{
			Role = role,
			Members = members,
			NonMembers = nonMembers
		};
		return View(model);
	}
	[HttpPost]
	public async Task<IActionResult> RoleEdit(RoleEditModel model)
	{
		foreach (var userId in model.IdsToAdd ?? new string[] { })
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var result = await _userManager.AddToRoleAsync(user, model.RoleName);
				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
		}
		foreach (var userId in model.IdsToDelete ?? new string[] { })
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
		}
		return RedirectToAction(nameof(RoleEdit));
	}
	public IActionResult RoleCreate()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> RoleCreate(RoleModel model)
	{
		if (ModelState.IsValid)
		{
			var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
			if (result.Succeeded)
			{
				return RedirectToAction(nameof(RoleList));
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
		}
		return View(model);
	}
}