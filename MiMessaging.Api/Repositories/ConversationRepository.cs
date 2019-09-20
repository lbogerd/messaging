using MiMessaging.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Repositories
{
	public class ConversationRepository
	{
		private readonly MiMessagingContext _context;

		public ConversationRepository(MiMessagingContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<FullConversationDto>> GetFullConversations()
		{
			// TODO: rework to minimize database calls?
			var allConversationIds = await _context
				.Conversations
				.Select(p => p.Id)
				.ToListAsync();

			var allFullConversations = new List<FullConversationDto>();
			foreach (var conversationId in allConversationIds)
			{
				allFullConversations.Add(await GetFullConversation(conversationId));
			}

			return allFullConversations;
		}

		public async Task<FullConversationDto> GetFullConversation(Guid id)
		{
			var conversation = await _context
				.Conversations
				.Include(p => p.Messages)
				.Include(p => p.Participants)
				.Include(p => p.ConversationStatusHistory)
				.SingleAsync(p => p.Id == id);

			// TODO: rework this to prevent dependency?
			var userRepo = new UserRepository(_context);
			var participants = new List<Guid>();

			foreach (var participant in conversation.Participants)
			{
				participants.Add(participant.ParticipantId);
			}

			// TODO: rework this to prevent dependency?
			var messageRepo = new MessageRepository(_context);
			var messages = new List<MessageDto>();

			foreach (var message in conversation.Messages)
			{
				messages.Add(await messageRepo
					.GetMessage(message.Id));
			}

			return new FullConversationDto
			{
				Id = conversation.Id,
				Title = conversation.Title,
				SalesforceId = conversation.InternalReference,
				StartedOn = conversation.StartedOn,
				CurrentStatus = conversation
					.ConversationStatusHistory
					.OrderByDescending(p => p.SetOn)
					.First()
					.ConversationStatusEnum
					.ToString(),
				Participants = participants,
				Messages = messages
			};
		}
	}
}
