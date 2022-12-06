using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnBookIT.Authentication;
using UnBookIT.Controllers;
using UnBookIT.Data;
using UnBookIT.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

#if DEBUG
	builder.Services.AddScoped<IBookITService, MockBookITService>();
#else
	builder.Services.AddScoped<IBookITService, BookITService>();
#endif

// Setup database connection
{
	var server = Environment.GetEnvironmentVariable("DATABASE_SERVER") ?? "db";
	var user = Environment.GetEnvironmentVariable("DATABASE_USER") ?? "bookIT";
	var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "password";
	var database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "bookIT";
	var version = Environment.GetEnvironmentVariable("DATABASE_VERSION") ?? "10.6.4-mariadb";
	builder.Services.AddDbContext<BookITContext>(options =>
		options.UseMySql(
			$"server={server};user={user};password={password};database={database};",
			ServerVersion.Parse(version)
		));
}

// Setup Gamma OAuth2
{
	const string GammaChallengeScheme = "Gamma";
	builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = GammaChallengeScheme;
		})
		.AddCookie()
		.AddOAuth<GammaAuthenticationOptions, GammaAuthenticationHandler>(GammaChallengeScheme, o => { });
}

BookingsController.RedirectURL = Environment.GetEnvironmentVariable("REDIRECT_URL") ?? "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
