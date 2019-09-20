using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiMessaging.Api.Mappers;
using MiMessaging.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController 
		: ControllerBase
	{
		private readonly MiMessagingContext _context;
		private readonly IdentityDomainMapper _identityDomainMapper;

		public UsersController(MiMessagingContext context)
		{
			_context = context;
			_identityDomainMapper = new IdentityDomainMapper();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
		{
			var allUsers = await _context
				.Users
				.ToListAsync();

			var userDtos = new List<UserDto>();
			foreach (var user in allUsers)
			{
				userDtos
					.Add(_identityDomainMapper
						.ToUserDto(user));
			}

			return userDtos;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> GetUser(Guid id)
		{
			var user = await _context
				.Users
				.Where(p => p.Id == id)
				.SingleAsync();

			if (user == null)
			{
				return NotFound();
			}

			return _identityDomainMapper
				.ToUserDto(user);
		}
	}
}
