using MiMessaging.Entities.IdentityDomain;
using System;

namespace MiMessaging.Api.Models
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserType { get; set; }
	}
}