using MiMessaging.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Repositories
{
	public class MessageRepository
	{
		private readonly MiMessagingContext _context;

		public MessageRepository(MiMessagingContext context)
		{
			_context = context;
		}

		public async Task<MessageDto> GetMessage(Guid id)
		{
			var message = await _context
				.Messages
				.FindAsync(id);

			// TODO: rework this to userrepo?
			var user = new UserDto
			{
				Id = message.SentById,
				FirstName = message.SentBy.FirstName,
				LastName = message.SentBy.LastName,
				UserType = message.SentBy.UserType.ToString()
			};

			return new MessageDto
			{
				Body = message.Body,
				SentOn = message.SentOn,
				SentBy = user.Id
			};
		}
	}
}
