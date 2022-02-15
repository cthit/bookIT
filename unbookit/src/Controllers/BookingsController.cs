using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnBookIT.Constraints;
using UnBookIT.Models;
using UnBookIT.Services;

namespace UnBookIT.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
[Produces("application/json")]
public class BookingsController : ControllerBase
{
	public static string RedirectURL { get; set; } = "";

	private readonly IBookITService service;

	public BookingsController(IBookITService service) =>
		(this.service) = (service);

	[HttpPost("{id}"), FormDelete]
	[ResponseCache(NoStore = true)]
	public IActionResult Delete(int id) =>
		service.Delete(id) switch
		{
			true => SeeOther(RedirectURL),
			false => NotFound(new Error($"Could not find booking with id {id}. If you think it should exists, please contact digIT.")),
		};

	public IActionResult SeeOther(string url)
	{
		Response.Headers.Add("Location", url);
		return StatusCode(StatusCodes.Status303SeeOther);
	}
}
