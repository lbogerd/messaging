using MiMessaging.Api.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiMessaging.Api.Repositories
{
	public class UserRepository
	{
		private readonly MiMessagingContext _context;

		public UserRepository(MiMessagingContext context)
		{
			_context = context;
		}

		public async Task<UserDto> GetUser(Guid id)
		{
			var user = await _context
				.Users
				.FindAsync(id);

			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserType = user.UserType.ToString()
			};
		}
	}
}
