using MiMessaging.Entities.IdentityDomain;
using System;
using System.Collections.Generic;

namespace MiMessaging.Entities.MessagingDomain
{
	public class Message
	{
		public Guid Id { get; set; }
		public string Body { get; set; }
		public DateTime SentOn { get; set; }

		public List<Attachment> Attachments { get; set; }
		public List<MessageReadStatus> MessageReadStatuses { get; set; }

		public Guid ConversationId { get; set; }
		public Conversation Conversation { get; set; }
		public Guid SentById { get; set; }
		public User SentBy { get; set; }
	}
}
