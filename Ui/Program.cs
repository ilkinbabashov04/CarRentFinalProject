using Azure.Storage.Files.Shares;
using Core.Tools;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
//{
//	opt.RequireHttpsMetadata = false;
//	opt.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidAudience = JwtTokenDefaults.ValidAudience,
//		ValidIssuer = JwtTokenDefaults.ValidIssuer,
//		ClockSkew = TimeSpan.Zero,
//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
//		ValidateLifetime = true,
//		ValidateIssuerSigningKey = true,
//	};
//});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Login s?hif?si
        options.LogoutPath = "/Login/LogOut"; // Ç?x?? s?hif?si
        options.AccessDeniedPath = "/Pages/AccessDenied"; // ?caz?siz giri?
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "CarBookJwt";
    });



var azureStorageConfig = builder.Configuration.GetSection("AzureStorage");
string connectionString = azureStorageConfig["ConnectionString"];
string shareName = azureStorageConfig["ShareName"];

builder.Services.AddSingleton(_ => new ShareServiceClient(connectionString));
builder.Services.AddSingleton(provider =>
{
    var serviceClient = provider.GetRequiredService<ShareServiceClient>();
    return serviceClient.GetShareClient(shareName);
});

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

app.UseRouting();

app.Use(async (context, next) =>
{
	string cookie = string.Empty;
	if (context.Request.Cookies.TryGetValue("Language", out cookie))
	{
		System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie);
		System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie);
	}
	else
	{
		System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
		System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
	}
	await next.Invoke();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
