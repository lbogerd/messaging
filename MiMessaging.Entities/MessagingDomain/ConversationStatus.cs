using System;

namespace MiMessaging.Entities.MessagingDomain
{
	public class ConversationStatus
	{
		public Guid Id { get; set; }
		public ConversationStatusEnum ConversationStatusEnum { get; set; }
		public DateTime SetOn { get; set; }

		public Guid ConversationId { get; set; }
		public Conversation Conversation { get; set; }
	}
}
