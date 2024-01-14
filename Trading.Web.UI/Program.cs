using Microsoft.AspNetCore.Identity;
using Trading.Repository.Layer.Abstract;
using Trading.Service.Layer.Email;
using Trading.Web.UI.Db;
using Trading.Web.UI.Dependensies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencyServices(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
	opt.UseSqlServer("Data Source=CANER\\SQLEXPRESS;Database=OnlineTradignDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});
builder.Services.AddIdentity<Person,IdentityRole>().AddEntityFrameworkStores< ApplicationDbContext >().AddDefaultTokenProviders();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "adminproductlist",
	pattern: "admin/products",
	defaults: new { controller = "Admin", action = "ProductList" }
	);
app.MapControllerRoute(
	name: "adminproductedit",
	pattern: "admin/products/{id?}",
	defaults: new { controller = "Admin", action = "EditProduct" }
	);
app.MapControllerRoute(
	name: "search",
	pattern: "search",
	defaults: new { controller = "Shop", action = "Search" }
	);
app.MapControllerRoute(
    name: "productdetails",
    pattern: "{url}",
    defaults: new { controller = "Shop", action = "Details" }
    );
app.MapControllerRoute(
	name:"products",
	pattern: "products/{category?}/{page?}",
	defaults:new {controller="Shop",action="List" }
	);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();