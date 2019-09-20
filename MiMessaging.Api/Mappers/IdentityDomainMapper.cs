using MiMessaging.Api.Models;
using MiMessaging.Entities.IdentityDomain;
using MiMessaging.Entities.MessagingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Mappers
{
	public class IdentityDomainMapper
	{
		public UserDto ToUserDto(User user)
		{
			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserType = user
					.UserType
					.ToString()
			};
		}
	}
}
