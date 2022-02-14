using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnBookIT.Models;
using UnBookIT.Services;

namespace UnBookIT.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
	private readonly ILogger<BookingsController> logger;
	private readonly IBookITService service;

	public BookingsController(ILogger<BookingsController> logger, IBookITService service)
	{
		this.logger = logger;
		this.service = service;
	}

	[HttpGet]
	public IEnumerable<Booking> GetAll() =>
		service.GetAll();

	[HttpGet("{id}")]
	public Booking? Get(int id) =>
		service.Get(id);

	[HttpDelete("{id}")]
	public bool Delete(int id) =>
		service.Delete(id);
}
