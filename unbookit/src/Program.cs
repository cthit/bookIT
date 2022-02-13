using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

var app = builder.Build();

var prefix = Environment.GetEnvironmentVariable("ROUTE_PREFIX") ?? "";
app.UsePathBase(prefix);
app.UseRouting();

app.MapControllers();
app.Run();
