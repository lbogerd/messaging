using MiMessaging.Api.Models;
using MiMessaging.Entities.MessagingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Mappers
{
	public class MessagingDomainMapper
	{
		public FullConversationDto ToFullConversationDto(Conversation conversation)
		{
			var messageDtos = new List<MessageDto>();
			foreach (var message in conversation.Messages)
			{
				messageDtos.Add(ToMessageDto(message));
			}

			var participantsIds = new List<Guid>();
			foreach (var conversationParticipant in conversation.Participants)
			{
				participantsIds.Add(conversationParticipant
					.ParticipantId);
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
				Participants = participantsIds,
				Messages = messageDtos
			};
		}

		public Conversation ToConversation(FullConversationDto fullConversationDto)
		{
			if (fullConversationDto.Id == null)
			{
				fullConversationDto.Id = new Guid();
			}

			var messages = new List<Message>();
			foreach (var messageDto in fullConversationDto.Messages)
			{
				messages.Add(ToMessage(messageDto));
			}

			var participants = new List<ConversationParticipant>();
			foreach (var userId in fullConversationDto.Participants)
			{
				participants.Add(new ConversationParticipant
				{
					ConversationId = fullConversationDto.Id.Value,
					ParticipantId = userId
				});
			}

			var statusHistory = new List<ConversationStatus>
			{
				new ConversationStatus
				{
					Id = new Guid(),
					ConversationId = fullConversationDto.Id.Value,
					ConversationStatusEnum = ConversationStatusEnum.New,
					SetOn = DateTime.UtcNow
				}
			};

			return new Conversation
			{
				Id = fullConversationDto.Id.Value,
				Title = fullConversationDto.Title,
				InternalReference = fullConversationDto.SalesforceId,
				StartedOn = fullConversationDto.StartedOn == null
					? DateTime.UtcNow
					: fullConversationDto.StartedOn.Value,
				ConversationStatusHistory = statusHistory,
				Messages = messages,
				Participants = participants
			};
		}

		public MessageDto ToMessageDto(Message message)
		{
			return new MessageDto
			{
				Id = message.Id,
				Body = message.Body,
				SentBy = message.SentById,
				SentOn = message.SentOn
			};
		}

		public Message ToMessage(MessageDto messageDto)
		{
			return new Message
			{
				Id = messageDto.Id == null
					? new Guid()
					: messageDto.Id.Value,
				Body = messageDto.Body,
				SentOn = messageDto.SentOn == null
					? DateTime.UtcNow
					: messageDto.SentOn.Value,
				SentById = messageDto.SentBy
			};
		}
	}
}
