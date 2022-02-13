using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UnBookIT.Models;

namespace UnBookIT.Services;

public class MockBookITService : IBookITService
{
	private static readonly IDictionary<int, Booking> bookings = new Dictionary<int, Booking>
	{
		[0] = new Booking
		{
			Id = 0,
			UserId = "user0",
			BeginDate = new DateTime(2020, 1, 1),
			EndDate = new DateTime(2020, 1, 2),
			Group = "group0",
			Description = "description0",
			RoomId = 0,
			CreatedAt = new DateTime(2020, 1, 1),
			UpdatedAt = new DateTime(2020, 1, 1),
			Title = "title0",
			Phone = "phone0",
			DeletedAt = null
		},
		[1] = new Booking
		{
			Id = 1,
			UserId = "user1",
			BeginDate = new DateTime(2020, 1, 3),
			EndDate = new DateTime(2020, 1, 4),
			Group = "group1",
			Description = "description1",
			RoomId = 1,
			CreatedAt = new DateTime(2020, 1, 1),
			UpdatedAt = new DateTime(2020, 1, 1),
			Title = "title1",
			Phone = "phone1",
			DeletedAt = null
		},
	};

	public IEnumerable<Booking> GetAll() =>
		bookings.Values;

	public Booking? Get(int id) =>
		bookings.TryGetValue(id, out Booking? booking) ? booking : null;

	public bool Delete(int id) =>
		false;
}
