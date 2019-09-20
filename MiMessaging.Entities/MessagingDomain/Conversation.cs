using MiMessaging.Entities.IdentityDomain;
using System;
using System.Collections.Generic;

namespace MiMessaging.Entities.MessagingDomain
{
	public class Conversation
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string InternalReference { get; set; }
		public DateTime StartedOn { get; set; }

		public List<Message> Messages { get; set; }
		public List<ConversationParticipant> Participants { get; set; }
		public List<ConversationStatus> ConversationStatusHistory { get; set; }
	}
}